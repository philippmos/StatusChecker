using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using StatusChecker.Models;

namespace StatusChecker.Services
{
    public class WebRequestService
    {
        private string _statusRequestUrl = "/status";

        private string _username = "";
        private string _password = "";

        private string _ipAddress = "";


        public string GetTemperatureForIpAddress(string ipAddress)
        {
            _ipAddress = ipAddress;

            WebRequestResponse webRequestResponse = SerializeWebResponse(GetWebResponse());



            return webRequestResponse != null ? webRequestResponse.Mac : "-";
        }

        private WebResponse GetWebResponse()
        {
            var request = WebRequest.Create($"http://{ _ipAddress }{ _statusRequestUrl }");
            request.Credentials = new NetworkCredential(_username, _password);

            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                var reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();
            }

            return response;

        }



        private WebRequestResponse SerializeWebResponse(WebResponse webResponse)
        {
            return new WebRequestResponse
            {
                Time = "",
                Mac = ""
            };
        }


    }
}
