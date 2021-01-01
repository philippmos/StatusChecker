using System;
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

        /// <summary>
        /// Get Min and Max Values for a specific GadgetId
        /// </summary>
        /// <param name="gadgetId"></param>
        /// <returns></returns>
        Task<Dictionary<string, KeyValuePair<double, DateTime>>> GetExtremepointsAsync(int gadgetId);

        /// <summary>
        /// Removes all Elements for specific Gadget
        /// </summary>
        /// <param name="gadgetId"></param>
        /// <returns></returns>
        Task<int> DeleteAllForGadgetAsync(int gadgetId);
    }
}
