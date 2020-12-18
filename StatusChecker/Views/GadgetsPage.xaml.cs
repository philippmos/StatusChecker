using System;
using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.ViewModels;

namespace StatusChecker.Views
{
    public partial class GadgetsPage : ContentPage
    {
        GadgetsViewModel viewModel;

        public GadgetsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new GadgetsViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var gadget = (GadgetViewModel)layout.BindingContext;
            await Navigation.PushAsync(new GadgetDetailPage(new GadgetDetailViewModel(gadget)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
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