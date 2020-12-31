using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.Models.Database;
using StatusChecker.Views.GadgetPages;
using StatusChecker.Helper;

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

                if (gadgets == null || gadgets.Count() == 0) return;


                List<Gadget> sortedGadgetList = await GadgetHelper.SortGadgetListBySetting(gadgets.ToList());

                foreach (var gadget in sortedGadgetList)
                {
                    GadgetStatus gadgetStatus = await _webRequestService.GetStatusAsync(gadget.IpAddress);

                    var statusIndicatorColor = GadgetHelper.GetStatusIndicatorColor(gadgetStatus);

                    if(gadgetStatus == null)
                    {
                        gadgetStatus = new GadgetStatus
                        {
                            temperature = 0.00,
                            overtemperature = false,
                            temperature_status = "undefined",
                            mac = "",
                            voltage = 0.00
                        };                       
                    }

                    var viewModel = new GadgetViewModel
                    {
                        Id = gadget.Id,
                        DeviceId = gadgetStatus.mac,
                        Name = gadget.Name,
                        Location = gadget.Location,
                        IpAddress = gadget.IpAddress,
                        Description = gadget.Description,
                        IsStatusOk = gadgetStatus.temperature_status == "Normal",
                        StatusIndicatorColor = statusIndicatorColor.ToString(),
                        TemperatureStatus = gadgetStatus.temperature_status,
                        Temperature = gadgetStatus.temperature,
                        TemperatureC = $"{gadgetStatus.temperature} °C",
                        Voltage = $"{ gadgetStatus.voltage } V"
                    };

                    if(gadgetStatus.temperature_status == "undefined")
                    {
                        viewModel.TemperatureC = "Status nicht verfügbar";
                    }


                    Gadgets.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                 var properties = new Dictionary<string, string> {
                    { "Method", "ExecuteLoadItemsCommand" },
                    { "Event", "Could not Add GadgetViewModel" }
                };

                AppHelper.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}