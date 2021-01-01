using System;
using System.Collections.Generic;
using StatusChecker.Infrastructure.Repositories;
using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Services.Interfaces;
using StatusChecker.ViewModels.Gadgets;
using Xamarin.Forms;

namespace StatusChecker.Services
{
    public class GadgetStatusRequestService : IGadgetStatusRequestService
    {
        #region Fields
        private readonly IRepository<GadgetStatusRequestRepository> _gadgetStatusRequestRepository;
        #endregion


        #region Construction
        public GadgetStatusRequestService()
        {
            _gadgetStatusRequestRepository = DependencyService.Get<IRepository<GadgetStatusRequestRepository>>();
        }
        #endregion

        #region Interface Methods
        public void SaveGadgetStatusRequest(GadgetViewModel gadgetViewModel)
        {
            throw new NotImplementedException();
        }

        public void SaveGadgetStatusRequests(List<GadgetViewModel> gadgetViewModelList)
        {
            foreach (GadgetViewModel gadgetViewModel in gadgetViewModelList)
            {
                SaveGadgetStatusRequest(gadgetViewModel);
            }
        }
        #endregion
    }
}
