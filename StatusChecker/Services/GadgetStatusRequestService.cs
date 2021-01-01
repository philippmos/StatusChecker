using System;
using System.Collections.Generic;

using Xamarin.Forms;

using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;
using StatusChecker.Services.Interfaces;
using StatusChecker.ViewModels.Gadgets;
using StatusChecker.Models;

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
        public async void SaveGadgetStatusRequest(GadgetStatusRequest gadgetStatusRequest)
        {
            await _gadgetStatusRequestRepository.SaveAsync(gadgetStatusRequest);
        }


        public void SaveGadgetStatusRequest(GadgetViewModel gadgetViewModel)
        {
            GadgetStatusRequest gadgetStatusRequest = MapGadgetViewModelToGadgetStatusRequest(gadgetViewModel);

            SaveGadgetStatusRequest(gadgetStatusRequest);
        }

        public void SaveGadgetStatusRequest(Gadget gadget, GadgetStatus gadgetStatus)
        {
            GadgetStatusRequest gadgetStatusRequest = new GadgetStatusRequest
            {
                GadgetId = gadget.Id,
                RequestDateTime = DateTime.Now,

                Temperature = 00.00,
                Voltage = 00.00,
                IsStatusRequestValid = false
            };

            if (gadgetStatus != null && gadgetStatus.temperature > 0.00)
            {
                gadgetStatusRequest.Temperature = gadgetStatus.temperature;
                gadgetStatusRequest.Voltage = gadgetStatus.voltage;
                gadgetStatusRequest.IsStatusRequestValid = true;
            }

            SaveGadgetStatusRequest(gadgetStatusRequest);
        }

        public void SaveGadgetStatusRequests(List<GadgetViewModel> gadgetViewModelList)
        {
            foreach (GadgetViewModel gadgetViewModel in gadgetViewModelList)
            {
                SaveGadgetStatusRequest(gadgetViewModel);
            }
        }
        #endregion

        #region Helper Methods
        // TODO: Implement and use AutoMapper
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
