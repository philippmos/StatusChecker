using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.Views;

namespace StatusChecker.ViewModels
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
                var newGadget = gadget as Gadget;

                Gadgets.Add(new GadgetViewModel {
                       Id = gadget.Id,
                       Name = gadget.Name,
                       IpAddress = gadget.IpAddress
                });

                

                await DataStore.AddItemAsync(newGadget);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Gadgets.Clear();
                var gadgets = await DataStore.GetItemsAsync(true);
                foreach (var gadget in gadgets)
                {
                    GadgetStatus gadgetStatus = await WebRequestService.GetStatusAsync(gadget.IpAddress);

                    var viewModel = new GadgetViewModel
                    {
                        Id = gadget.Id,
                        Name = gadget.Name,
                        IpAddress = gadget.IpAddress,
                        IsStatusOk = gadgetStatus.temperature_status == "Normal",
                        Temperature = gadgetStatus.temperature,
                        TemperatureC = $"{gadgetStatus.temperature} °C"
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

        private Gadget MapGadgetViewModelToGadget(GadgetViewModel gadgetViewModel)
        {
            return new Gadget
            {
                Id = gadgetViewModel.Id,
                Name = gadgetViewModel.Name,
                IpAddress = gadgetViewModel.IpAddress,
                Description = gadgetViewModel.Description
            };
        }
    }
}