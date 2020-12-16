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

        void CheckupButton_Clicked(System.Object sender, System.EventArgs e)
        {
            RunStatusCheckup();
        }

        async void EnableAutoRefreshButton_Clicked(System.Object sender, System.EventArgs e)
        {
            const int numberOfRuns = 5;

            for(int i = 0; i < numberOfRuns; i++)
            {
                _btnAutoRefresh.Text = $"Auto-Refresh ({ numberOfRuns - i })";

                await _pbAutoRefreshIndicator.ProgressTo(0, 250, Easing.BounceOut);

                RunStatusCheckup();

                await _pbAutoRefreshIndicator.ProgressTo(1, 20000, Easing.Linear);
            }

            await _pbAutoRefreshIndicator.ProgressTo(0, 250, Easing.BounceOut);

            _btnAutoRefresh.Text = "Auto-Refresh aktivieren";
        }

        private async void RunStatusCheckup()
        {
            ToggleActivityIndicator(_checkupIndicator);

            var webRequestService = new WebRequestService();

            var gadgetConfigs = new Dictionary<string, Label>()
            {
            };

            ResetStatusLabels(gadgetConfigs.Select(x => x.Value).ToList());

            foreach (KeyValuePair<string, Label> gadgetConfig in gadgetConfigs)
            {
                var gadgetStatus = await webRequestService.GetStatusAsync(gadgetConfig.Key);
                if (gadgetStatus == null) continue;

                gadgetConfig.Value.Text = $"{ gadgetStatus.temperature } °C  ({ gadgetStatus.temperature_status })";
            }

            ToggleActivityIndicator(_checkupIndicator);
        }


        void ToggleActivityIndicator(ActivityIndicator activityIndicator)
        {
            activityIndicator.IsEnabled = !activityIndicator.IsEnabled;
            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }

        void ResetStatusLabels(List<Label> labelList)
        {
            foreach(Label label in labelList)
            {
                label.Text = "- °C";
            }
        }
    }
}
