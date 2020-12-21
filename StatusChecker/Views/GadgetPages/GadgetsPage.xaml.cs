using System;

using Xamarin.Forms;

using StatusChecker.ViewModels;

namespace StatusChecker.Views.GadgetPages
{
    public partial class GadgetsPage : ContentPage
    {
        private readonly GadgetsViewModel viewModel;

        public GadgetsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new GadgetsViewModel();
        }

        private async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var gadget = (GadgetViewModel)layout.BindingContext;
            await Navigation.PushAsync(new GadgetDetailPage(new GadgetDetailViewModel(gadget)));
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewGadgetPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Gadgets.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}