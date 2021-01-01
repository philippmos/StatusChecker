using System;
using System.Collections.Generic;

using Xamarin.Forms;

using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;
using StatusChecker.Services.Interfaces;
using StatusChecker.ViewModels.Gadgets;

namespace StatusChecker.Services
{
    public class GadgetStatusRequestService : IGadgetStatusRequestService
    {
        #region Fields
        private readonly IRepository<GadgetStatusRequest> _gadgetStatusRequestRepository;
        #endregion


        #region Construction
        public GadgetStatusRequestService()
        {
            _gadgetStatusRequestRepository = DependencyService.Get<IRepository<GadgetStatusRequest>>();
        }
        #endregion


        #region Interface Methods
        public async void SaveGadgetStatusRequestAsync(GadgetViewModel gadgetViewModel)
        {
            GadgetStatusRequest gadgetStatusRequest = MapGadgetViewModelToGadgetStatusRequest(gadgetViewModel);

            await _gadgetStatusRequestRepository.SaveAsync(gadgetStatusRequest);
        }

        public void SaveGadgetStatusRequests(List<GadgetViewModel> gadgetViewModelList)
        {
            foreach (GadgetViewModel gadgetViewModel in gadgetViewModelList)
            {
                SaveGadgetStatusRequestAsync(gadgetViewModel);
            }
        }
        #endregion

        #region Helper Methods
        private GadgetStatusRequest MapGadgetViewModelToGadgetStatusRequest(GadgetViewModel gadgetViewModel)
        {
            if (gadgetViewModel == null) return null;

            var gadgetStatusRequest = new GadgetStatusRequest
            {
                GadgetId = gadgetViewModel.Id,
                RequestDateTime = new DateTime(),
                Temperature = gadgetViewModel.Temperature,
                Voltage = gadgetViewModel.Voltage
            };


            gadgetStatusRequest.IsStatusRequestValid = false;

            if (gadgetViewModel.IsStatusOk && gadgetViewModel.Temperature > 0.00)
            {
                gadgetStatusRequest.IsStatusRequestValid = true;
            }


            return gadgetStatusRequest;

        }
        #endregion
    }
}
