﻿using Xamarin.Forms;

namespace StatusChecker.Views.LegalPages
{
    public partial class AppInfoPage : ContentPage
    {
        public AppInfoPage()
        {
            InitializeComponent();

            _lblVersionInfo.Text = App.GetAppVersionInformation();
        }

        async void _btnImprint_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ImprintPage { Title = "Impressum" });
        }

        async void _btnPrivacyPolicy_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PrivacyPolicyPage { Title = "Datenschutz" });
        }
    }
}
