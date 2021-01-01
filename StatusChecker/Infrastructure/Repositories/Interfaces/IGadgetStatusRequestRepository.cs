using System.Collections.Generic;
using System.Threading.Tasks;
using StatusChecker.Models.Database;

namespace StatusChecker.Infrastructure.Repositories.Interfaces
{
    public interface IGadgetStatusRequestRepository : IRepository<GadgetStatusRequest>
    {
        /// <summary>
        /// Get all GadgetStatusRequests for a specific GadgetId, that are valid
        /// </summary>
        /// <param name="gadgetId"></param>
        /// <returns></returns>
        Task<List<GadgetStatusRequest>> GetAllValidStatusRequestsForGadgetIdAsync(int gadgetId);
    }
}
