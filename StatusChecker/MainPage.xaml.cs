using System;
using Xamarin.Forms;

using StatusChecker.Services;
using System.Collections.Generic;

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
            var webRequestService = new WebRequestService();

            var gadgetConfigs = new Dictionary<string, Label>()
            {
            };


            foreach(KeyValuePair<string, Label> gadgetConfig in gadgetConfigs)
            {
                var gadgetStatus = webRequestService.GetStatus(gadgetConfig.Key);
                if (gadgetStatus == null) continue;

                gadgetConfig.Value.Text = $"{ gadgetStatus.temperature } °C";
            }


        }
    }
}
