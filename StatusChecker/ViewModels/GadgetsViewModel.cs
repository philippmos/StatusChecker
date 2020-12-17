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
        public ObservableCollection<Gadget> Gadgets { get; set; }
        public Command LoadItemsCommand { get; set; }

        public GadgetsViewModel()
        {
            Title = "Übersicht";
            Gadgets = new ObservableCollection<Gadget>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewGadgetPage, Gadget>(this, "AddItem", async (obj, gadget) =>
            {
                var newGadget = gadget as Gadget;
                Gadgets.Add(newGadget);
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
                foreach (var item in gadgets)
                {
                    Gadgets.Add(item);
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