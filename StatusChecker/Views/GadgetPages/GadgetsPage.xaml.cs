using System;

using Xamarin.Forms;

using StatusChecker.ViewModels.Gadgets;
using StatusChecker.Services.Interfaces;

namespace StatusChecker.Views.GadgetPages
{
    public partial class GadgetsPage : ContentPage
    {
        #region Fields
        private readonly GadgetsViewModel viewModel;
        private readonly IGadgetStatusRequestService _gadgetStatusRequestService = DependencyService.Get<IGadgetStatusRequestService>();
        #endregion


        #region Construction
        public GadgetsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new GadgetsViewModel();
        }
        #endregion


        #region View Handler
        private async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var gadget = (GadgetViewModel)layout.BindingContext;

            GadgetAnalyticsViewModel gadgetAnalyticsViewModel = await _gadgetStatusRequestService.GetGadgetAnalyticsViewModelForGadgetAsync(gadget.Id);

            await Navigation.PushAsync(new GadgetDetailPage(new GadgetDetailViewModel(gadget, gadgetAnalyticsViewModel)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Gadgets.Count == 0)
            {
                viewModel.IsBusy = true;
            }

        }
        #endregion


        #region View Events
        /// <summary>
        /// Open AddItem View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewGadgetPage()));
        }
        #endregion
    }
}