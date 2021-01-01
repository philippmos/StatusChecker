using System.Collections.Generic;
using System.Threading.Tasks;

using StatusChecker.Models;
using StatusChecker.Models.Database;
using StatusChecker.ViewModels.Gadgets;

namespace StatusChecker.Services.Interfaces
{
    public interface IGadgetStatusRequestService
    {
        /// <summary>
        /// Saves a single GadgetViewModel in Database for GadgetStatusRequest
        /// </summary>
        /// <param name="gadgetStatusRequest"></param>
        void SaveGadgetStatusRequest(GadgetStatusRequest gadgetStatusRequest);

        /// <summary>
        /// Saves a single GadgetViewModel in Database for GadgetStatusRequest
        /// </summary>
        /// <param name="gadgetViewModel"></param>
        void SaveGadgetStatusRequest(GadgetViewModel gadgetViewModel);

        /// <summary>
        /// Saves a single GadgetViewModel in Database for GadgetStatusRequest
        /// </summary>
        /// <param name="gadget"></param>
        /// <param name="gadgetStatus"></param>
        void SaveGadgetStatusRequest(Gadget gadget, GadgetStatus gadgetStatus);

        /// <summary>
        /// Saves multiple GadgetViewModels in Database for GadgetStatusRequest
        /// </summary>
        /// <param name="gadgetViewModelList"></param>
        void SaveGadgetStatusRequests(List<GadgetViewModel> gadgetViewModelList);

        /// <summary>
        /// Calcualtes the Average Temperature for all available StatusRequests
        /// </summary>
        /// <param name="gadgetId"></param>
        /// <returns></returns>
        Task<double> GetStatusRequestAverageTemperatureAsync(int gadgetId);
    }
}
