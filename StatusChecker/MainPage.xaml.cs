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


            var random = new Random();

            _temp_1.Text = $"{ webRequestService.GetTemperatureForIpAddress("") } °C";
            _temp_2.Text = $"{ random.Next(0, 100) } °C";
            _temp_3.Text = $"{ random.Next(0, 100) } °C";

        }
    }
}
