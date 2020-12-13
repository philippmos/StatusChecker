using System;
using Xamarin.Forms;

using StatusChecker.Services;

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

            var gadgetStatus01 = webRequestService.GetStatus("");

            if (gadgetStatus01 != null)
            {
                _temp_1.Text = $"{ gadgetStatus01.temperature } °C";
            }


        }
    }
}
