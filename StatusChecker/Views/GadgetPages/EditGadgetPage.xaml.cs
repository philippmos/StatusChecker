using Xamarin.Forms;

using StatusChecker.Models.Database;
using StatusChecker.ViewModels.Gadgets;
using StatusChecker.DataStore.Interfaces;

namespace StatusChecker.Views.GadgetPages
{
    public partial class EditGadgetPage : ContentPage
    {
        private IDataStore<Gadget> _dataStore => DependencyService.Get<IDataStore<Gadget>>();

        public Gadget Gadget { get; set; }

        private readonly GadgetDetailViewModel viewModel;

        public EditGadgetPage(GadgetDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public EditGadgetPage()
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

        private async void Save_Clicked(object sender, System.EventArgs e)
        {
            var updatedGadget = new Gadget
            {
                Id = viewModel.Gadget.Id,
                Name = viewModel.Gadget.Name,
                Location = viewModel.Gadget.Location,
                IpAddress = viewModel.Gadget.IpAddress,
                Description = viewModel.Gadget.Description
            };


            if (await _dataStore.UpdateAsync(updatedGadget))
            {
                Application.Current.MainPage = new MainPage();
            }
        }

        private void Cancel_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}