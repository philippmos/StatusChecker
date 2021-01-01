using System.Collections.Generic;
using StatusChecker.ViewModels.Gadgets;

namespace StatusChecker.Services.Interfaces
{
    public interface IGadgetStatusRequestService
    {
        /// <summary>
        /// Saves a single GadgetViewModel in Database for GadgetStatusRequest
        /// </summary>
        /// <param name="gadgetViewModel"></param>
        void SaveGadgetStatusRequestAsync(GadgetViewModel gadgetViewModel);

        /// <summary>
        /// Saves multiple GadgetViewModels in Database for GadgetStatusRequest
        /// </summary>
        /// <param name="gadgetViewModelList"></param>
        void SaveGadgetStatusRequests(List<GadgetViewModel> gadgetViewModelList);
    }
}
