using System.Threading.Tasks;
using StatusChecker.Models;
using StatusChecker.Models.Database;

namespace StatusChecker.Services.Interfaces
{
    public interface IWebRequestService
    {
        /// <summary>
        /// Get the GadgetStatus Model from WebRequest
        /// </summary>
        /// <param name="gadget"></param>
        /// <returns></returns>
        Task<GadgetStatus> GetStatusAsync(Gadget gadget);
    }
}
