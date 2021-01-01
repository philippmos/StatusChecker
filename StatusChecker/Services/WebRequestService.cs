using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.Services.Interfaces;
using StatusChecker.Models.Enums;
using StatusChecker.Helper;
using StatusChecker.Models.Database;

namespace StatusChecker.Services
{
    public class WebRequestService : IWebRequestService
    {
        #region Fields
        private readonly ISettingService _settingService;
        private readonly IGadgetStatusRequestService _gadgetStatusRequestService;
        #endregion


        #region Construction
        public WebRequestService()
        {
            _settingService = DependencyService.Get<ISettingService>();
            _gadgetStatusRequestService = DependencyService.Get<IGadgetStatusRequestService>();
        }
        #endregion


        #region Interface Methods
        public async Task<GadgetStatus> GetStatusAsync(Gadget gadget)
        {
            GadgetStatus gadgetStatus = SerializeWebResponse<GadgetStatus>(await ManageWebRequestAndReturnResponse(gadget.IpAddress));

            _gadgetStatusRequestService.SaveGadgetStatusRequest(gadget, gadgetStatus);

            return gadgetStatus ?? null;
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Manage the WebRequest and return the Resposne via JSON-Content
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private async Task<string> ManageWebRequestAndReturnResponse(string ipAddress)
        {
            var statusRequestUrl = await _settingService.GetSettingValueAsync(SettingKeys.StatusRequestUrl);

            var requestUrl = $"http://{ ipAddress }{ statusRequestUrl }";

            try
            {
                return await ProceedWebRequestAndGetResponseContent(requestUrl);
            }
            catch(Exception ex)
            {
                ProceedErrorHandling(ex, requestUrl);

                return null;
            }            
        }

        /// <summary>
        /// Runs the WebRequest and returns the JSON-Content
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        private async Task<string> ProceedWebRequestAndGetResponseContent(string requestUrl)
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

        /// <summary>
        /// Deserialize the JSON WebResponse to GadgetStatus
        /// </summary>
        /// <param name="serverResponse"></param>
        /// <returns></returns>
        private T SerializeWebResponse<T>(string serverResponse)
        {
            if (serverResponse == null) return default;

            try
            {
                return JsonSerializer.Deserialize<T>(serverResponse);
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string> {
                    { "Method", "SerializeWebResponse" },
                    { "Event", "Could not deserialize ServerResponse" }
                };

                AppHelper.TrackError(ex, properties);

                return default;
            }

        }

        /// <summary>
        /// Do the Error Handling
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="requestUrl"></param>
        private async void ProceedErrorHandling(Exception ex, string requestUrl)
        {
            var notifyWhenStatusNotRespond = await _settingService.GetSettingValueAsync(SettingKeys.NotifyWhenStatusNotRespond);

            if (notifyWhenStatusNotRespond == "1")
            {
                await Application.Current.MainPage.DisplayAlert("Status konnte nicht abgefragt werden", $"Adresse: { requestUrl }", "Schade");
            }

            var properties = new Dictionary<string, string> {
                    { "Method", "GetWebResponseAsync" },
                    { "Event", "Could not proceed WebRequest" }
                };

            AppHelper.TrackError(ex, properties);
        }
        #endregion
    }
}
