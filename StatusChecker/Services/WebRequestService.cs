using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using StatusChecker.Models;
using StatusChecker.Services.Interfaces;

namespace StatusChecker.Services
{
    public class WebRequestService : IWebRequestService
    {
        private readonly string _statusRequestUrl = "/status";


        public async Task<GadgetStatus> GetStatusAsync(string ipAddress)
        {
            GadgetStatus gadgetStatus = SerializeWebResponse(await GetWebResponseAsync(ipAddress));

            return gadgetStatus ?? null;
        }

        private async Task<string> GetWebResponseAsync(string ipAddress)
        {
            var request = WebRequest.Create($"http://{ ipAddress }{ _statusRequestUrl }");

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
