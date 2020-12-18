using System.Threading.Tasks;
using StatusChecker.Models;

namespace StatusChecker.Services.Interfaces
{
    public interface IWebRequestService
    {
        Task<GadgetStatus> GetStatusAsync(string ipAddress);
    }
}
