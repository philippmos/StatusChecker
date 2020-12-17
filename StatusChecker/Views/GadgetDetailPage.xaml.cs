using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.ViewModels;

namespace StatusChecker.Views
{
    public partial class GadgetDetailPage : ContentPage
    {
        GadgetDetailViewModel viewModel;

        public GadgetDetailPage(GadgetDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public GadgetDetailPage()
        {
            InitializeComponent();

            var gadget = new Gadget
            {
                Name = "Gerät 1",
                IpAddress = ""
            };

            viewModel = new GadgetDetailViewModel(gadget);
            BindingContext = viewModel;
        }
    }
}