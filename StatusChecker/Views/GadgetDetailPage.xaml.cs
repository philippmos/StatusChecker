using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.ViewModels;
using StatusChecker.Services.Interfaces;

namespace StatusChecker.Views
{
    public partial class GadgetDetailPage : ContentPage
    {
        private IDataStore<Gadget> _dataStore => DependencyService.Get<IDataStore<Gadget>>();

        GadgetDetailViewModel viewModel;

        public GadgetDetailPage(GadgetDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public GadgetDetailPage()
        {
            InitializeComponent();

            var gadget = new GadgetViewModel
            {
                Name = "",
                IpAddress = ""
            };

            viewModel = new GadgetDetailViewModel(gadget);
            BindingContext = viewModel;
        }

        async void RemoveGadget_Clicked(System.Object sender, System.EventArgs e)
        {
            await _dataStore.DeleteItemAsync(viewModel.Gadget.Id);
            await Navigation.PushAsync(new GadgetsPage());
        }

        async void EditGadget_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EditGadgetPage(viewModel));
        }
    }
}