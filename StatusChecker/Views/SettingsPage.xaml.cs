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
        private SettingsViewModel _viewModel;

        private readonly IRepository<Setting> _settingRepository = DependencyService.Get<IRepository<Setting>>();


        public SettingsPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            _viewModel = new SettingsViewModel()
            {
                Title = "Einstellungen"
            };


            var statusRequestUrl = await _settingRepository.GetAsync((int)SettingKeys.StatusRequestUrl);

            if (statusRequestUrl != null)
            {
                _viewModel.StatusRequestUrl = statusRequestUrl.Value;
            }


            BindingContext = _viewModel;
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
