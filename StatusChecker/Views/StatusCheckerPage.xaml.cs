using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

using StatusChecker.Services.Interfaces;
using StatusChecker.Models;

namespace StatusChecker.Views
{
    public partial class StatusCheckerPage : ContentPage
    {

        private IDataStore<Gadget> _dataStore => DependencyService.Get<IDataStore<Gadget>>();
        private IWebRequestService _webRequestService => DependencyService.Get<IWebRequestService>();

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

            List<Label> labelList = new List<Label>
            {
                _temp_1, _temp_2, _temp_3, _temp_4, _temp_5
            };

            var gadgets = await _dataStore.GetItemsAsync();
            var gadgetConfigs = new Dictionary<string, Label>();

            for(int i = 0; i < labelList.Count(); i++)
            {
                gadgetConfigs.Add(gadgets.ElementAt(i).IpAddress, labelList[i]);
            }


            ResetStatusLabels(gadgetConfigs.Select(x => x.Value).ToList());

            foreach (KeyValuePair<string, Label> gadgetConfig in gadgetConfigs)
            {
                var gadgetStatus = await _webRequestService.GetStatusAsync(gadgetConfig.Key);
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
