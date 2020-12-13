using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

using StatusChecker.Models;



namespace StatusChecker.Services
{
    public class WebRequestService
    {
        private string _statusRequestUrl = "/status";

        private string _username = "";
        private string _password = "";

        private string _ipAddress = "";


        public GadgetStatus GetStatus(string ipAddress)
        {
            _ipAddress = ipAddress;

            GadgetStatus gadgetStatus = SerializeWebResponse(GetWebResponse());

            return gadgetStatus ?? null;
        }

        private string GetWebResponse()
        {
            var request = WebRequest.Create($"http://{ _ipAddress }{ _statusRequestUrl }");
            request.Credentials = new NetworkCredential(_username, _password);

            WebResponse response = request.GetResponse();

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
