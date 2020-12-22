using System;
using System.Collections.Generic;
using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;
using StatusChecker.ViewModels;
using Xamarin.Forms;

namespace StatusChecker.Views
{
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsViewModel viewModel;

        private readonly IRepository<Setting> _settingRepository = DependencyService.Get<IRepository<Setting>>();


        public SettingsPage()
        {
            InitializeComponent();


            BindingContext = this.viewModel = new SettingsViewModel
            {
                Title = "Einstellungen",
                StatusRequestUrl = "/status2"
            };
        }

        protected override void OnAppearing()
        {

        }

        private async void Save_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void Cancel_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }
}
