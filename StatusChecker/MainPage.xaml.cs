using System;
using Xamarin.Forms;

using StatusChecker.Services;
using System.Collections.Generic;
using System.Linq;

namespace StatusChecker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void CheckupButton_Clicked(System.Object sender, System.EventArgs e)
        {
            ToggleActivityIndicator(_checkupIndicator);

            var webRequestService = new WebRequestService();

            var gadgetConfigs = new Dictionary<string, Label>()
            {
            };

            ResetStatusLabels(gadgetConfigs.Select(x => x.Value).ToList());

            foreach(KeyValuePair<string, Label> gadgetConfig in gadgetConfigs)
            {
                var gadgetStatus = await webRequestService.GetStatusAsync(gadgetConfig.Key);
                if (gadgetStatus == null) continue;

                gadgetConfig.Value.Text = $"{ gadgetStatus.temperature } °C";
            }

            ToggleActivityIndicator(_checkupIndicator);
        }


        void ToggleActivityIndicator(ActivityIndicator activityIndicator)
        {
            activityIndicator.IsEnabled = !activityIndicator.IsEnabled;
            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }
    }
}
