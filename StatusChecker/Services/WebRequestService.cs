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
        private string _statusRequestUrl = "/status";

        private string _username = "";
        private string _password = "";

        private string _ipAddress = "";


        public async Task<GadgetStatus> GetStatusAsync(string ipAddress)
        {
            _ipAddress = ipAddress;

            GadgetStatus gadgetStatus = SerializeWebResponse(await GetWebResponseAsync());

            return gadgetStatus ?? null;
        }

        private async Task<string> GetWebResponseAsync()
        {
            var request = WebRequest.Create($"http://{ _ipAddress }{ _statusRequestUrl }");
            request.Credentials = new NetworkCredential(_username, _password);

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
