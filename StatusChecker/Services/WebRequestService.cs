using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;

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

            var requestUrl = $"http://{ ipAddress }{ statusRequestUrlSetting.Value }";

            try
            {
                var request = WebRequest.Create(requestUrl);

                int.TryParse(AppSettingsManager.Settings["WebRequestTimeout"], out int webRequestTimeout);

                request.Timeout = webRequestTimeout;

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
            catch(Exception ex)
            {
                var notifyWhenStatusNotRespond = await _settingRepository.GetAsync((int)SettingKeys.NotifyWhenStatusNotRespond);
                if (notifyWhenStatusNotRespond != null && notifyWhenStatusNotRespond.Value == "1")
                {
                    await Application.Current.MainPage.DisplayAlert("Status konnte nicht abgefragt werden", $"Adresse: { requestUrl }", "Schade");
                }
                

                var properties = new Dictionary<string, string> {
                    { "Method", "GetWebResponseAsync" },
                    { "Event", "Could not proceed WebRequest" }
                };

                if(App.PermissionTrackErrors)
                {
                    Crashes.TrackError(ex, properties);
                }

                Debug.WriteLine(ex.Message);

                return null;
            }
            
        }



        private GadgetStatus SerializeWebResponse(string serverResponse)
        {
            if (serverResponse == null) return null;

            try
            {
                return JsonSerializer.Deserialize<GadgetStatus>(serverResponse);
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string> {
                    { "Method", "SerializeWebResponse" },
                    { "Event", "Could not deserialize ServerResponse" }
                };

                if (App.PermissionTrackErrors)
                {
                    Crashes.TrackError(ex, properties);
                }


                Debug.WriteLine(ex.Message);

                return null;
            }

        }


    }
}
