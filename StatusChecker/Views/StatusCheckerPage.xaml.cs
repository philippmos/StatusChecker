using Xamarin.Forms;

using StatusChecker.Services;
using System.Collections.Generic;
using System.Linq;

namespace StatusChecker.Views
{
    public partial class StatusCheckerPage : ContentPage
    {
        public StatusCheckerPage()
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

            ToggleManualCheckupButton();

            for(int i = 0; i < numberOfRuns; i++)
            {
                _btnAutoRefresh.Text = $"Auto-Refresh ({ numberOfRuns - i })";

                await _pbAutoRefreshIndicator.ProgressTo(0, 250, Easing.BounceOut);

                RunStatusCheckup();

                await _pbAutoRefreshIndicator.ProgressTo(1, 20000, Easing.Linear);
            }

            await _pbAutoRefreshIndicator.ProgressTo(0, 250, Easing.BounceOut);

            ToggleManualCheckupButton();

            _btnAutoRefresh.Text = "Auto-Refresh aktivieren";
        }

        private async void RunStatusCheckup()
        {
            ToggleActivityIndicator(_checkupIndicator);

            var webRequestService = new WebRequestService();

            var gadgetConfigs = new Dictionary<string, Label>()
            {
                { "192.168.0.17", _temp_1 },
                { "192.168.0.38", _temp_2 },
                { "192.168.0.95", _temp_3 },
                { "192.168.0.178", _temp_4 },
                { "192.168.0.248", _temp_5 }
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

        void ToggleManualCheckupButton()
        {
            _btnCheckup.IsEnabled = !_btnCheckup.IsEnabled;
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
