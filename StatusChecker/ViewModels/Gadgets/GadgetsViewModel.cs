using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.Models.Database;
using StatusChecker.Views.GadgetPages;

namespace StatusChecker.ViewModels.Gadgets
{
    public class GadgetsViewModel : BaseViewModel
    {
        public ObservableCollection<GadgetViewModel> Gadgets { get; set; }
        public Command LoadItemsCommand { get; set; }

        public GadgetsViewModel()
        {
            Title = "Übersicht";
            Gadgets = new ObservableCollection<GadgetViewModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewGadgetPage, Gadget>(this, "AddItem", async (obj, gadget) =>
            {
                var newGadget = gadget;

                Gadgets.Add(new GadgetViewModel {
                       Id = gadget.Id,
                       Name = gadget.Name,
                       Location = gadget.Location,
                       IpAddress = gadget.IpAddress,
                       Description = gadget.Description
                });

                

                await _gadgetDataStore.AddAsync(newGadget);
            });
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Gadgets.Clear();
                var gadgets = await _gadgetDataStore.GetAllAsync(true);
                foreach (var gadget in gadgets)
                {
                    GadgetStatus gadgetStatus = await _webRequestService.GetStatusAsync(gadget.IpAddress);

                    var viewModel = new GadgetViewModel
                    {
                        Id = gadget.Id,
                        DeviceId = gadgetStatus.mac,
                        Name = gadget.Name,
                        Location = gadget.Location,
                        IpAddress = gadget.IpAddress,
                        Description = gadget.Description,
                        IsStatusOk = gadgetStatus.temperature_status == "Normal",
                        TemperatureStatus = gadgetStatus.temperature_status,
                        Temperature = gadgetStatus.temperature,
                        TemperatureC = $"{gadgetStatus.temperature} °C",
                        Voltage = $"{ gadgetStatus.voltage } V"
                    };

                    Gadgets.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}