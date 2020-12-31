using System.Threading.Tasks;
using StatusChecker.Models;

namespace StatusChecker.Services.Interfaces
{
    public interface IWebRequestService
    {
        /// <summary>
        /// Get the GadgetStatus Model from WebRequest
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<GadgetStatus> GetStatusAsync(string ipAddress);
    }
}
