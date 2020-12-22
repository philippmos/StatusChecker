using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;

using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models;
using StatusChecker.Models.Database;
using StatusChecker.Services.Interfaces;

namespace StatusChecker.Services
{
    public class WebRequestService : IWebRequestService
    {
        private readonly IRepository<Setting> _settingRepository = DependencyService.Get<IRepository<Setting>>();

        public async Task<GadgetStatus> GetStatusAsync(string ipAddress)
        {
            GadgetStatus gadgetStatus = SerializeWebResponse(await GetWebResponseAsync(ipAddress));

            return gadgetStatus ?? null;
        }

        private async Task<string> GetWebResponseAsync(string ipAddress)
        {
            var statusRequestUrlSetting = await _settingRepository.GetAsync((int)SettingKeys.StatusRequestUrl);

            if (statusRequestUrlSetting == null) return null;


            var request = WebRequest.Create($"http://{ ipAddress }{ statusRequestUrlSetting.Value }");

            request.Credentials = new NetworkCredential(
                AppSettingsManager.Settings["WebRequestUsername"],
                AppSettingsManager.Settings["WebRequestPassword"]);

            WebResponse response = await request.GetResponseAsync();

            using (Stream dataStream = response.GetResponseStream())
            {
                var reader = new StreamReader(dataStream);

                return reader.ReadToEnd();
            }
        }



        private GadgetStatus SerializeWebResponse(string serverResponse)
        {
            if (serverResponse == null) return null;

            var deserializedResponse = JsonSerializer.Deserialize<GadgetStatus>(serverResponse);


            return deserializedResponse;
        }


    }
}
