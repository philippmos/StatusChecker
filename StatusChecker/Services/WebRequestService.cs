using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using StatusChecker.Models;
using StatusChecker.Models.Database;
using StatusChecker.Services.Interfaces;

namespace StatusChecker.Services
{
    public class WebRequestService : IWebRequestService
    {
        private readonly ISettingService _settingService;

        public WebRequestService()
        {
            _settingService = DependencyService.Get<ISettingService>();
        }

        public async Task<GadgetStatus> GetStatusAsync(string ipAddress)
        {
            GadgetStatus gadgetStatus = SerializeWebResponse(await GetWebResponseAsync(ipAddress));

            return gadgetStatus ?? null;
        }

        private async Task<string> GetWebResponseAsync(string ipAddress)
        {
            var statusRequestUrl = await _settingService.GetSettingValueAsync(SettingKeys.StatusRequestUrl);

            var requestUrl = $"http://{ ipAddress }{ statusRequestUrl }";

            try
            {
                var request = WebRequest.Create(requestUrl);

                var requestTimeoutInSeconds = await _settingService.GetSettingValueAsync(SettingKeys.RequestTimeoutInSeconds);
                int.TryParse(requestTimeoutInSeconds, out int requestTimeout);

                request.Timeout = requestTimeout * 1000;

                string webRequestUsername = AppSettingsManager.Settings["WebRequestUsername"];
                string webRequestPassword = AppSettingsManager.Settings["WebRequestPassword"];

                if (!string.IsNullOrEmpty(webRequestUsername) && !string.IsNullOrEmpty(webRequestPassword))
                {
                    request.Credentials = new NetworkCredential(webRequestUsername, webRequestPassword);
                }

                WebResponse response = await request.GetResponseAsync();

                using (Stream dataStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(dataStream);

                    return reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                var notifyWhenStatusNotRespond = await _settingService.GetSettingValueAsync(SettingKeys.NotifyWhenStatusNotRespond);

                if(notifyWhenStatusNotRespond == "1")
                {
                    await Application.Current.MainPage.DisplayAlert("Status konnte nicht abgefragt werden", $"Adresse: { requestUrl }", "Schade");
                }


                var properties = new Dictionary<string, string> {
                    { "Method", "GetWebResponseAsync" },
                    { "Event", "Could not proceed WebRequest" }
                };

                App.TrackError(ex, properties);

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

                App.TrackError(ex, properties);

                return null;
            }

        }


    }
}
