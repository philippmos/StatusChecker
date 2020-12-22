using Xamarin.Essentials;
using Xamarin.Forms;

using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;
using StatusChecker.ViewModels;

namespace StatusChecker.Views
{
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel _viewModel;

        private readonly IRepository<Setting> _settingRepository = DependencyService.Get<IRepository<Setting>>();


        public SettingsPage()
        {
            InitializeComponent();

            VersionTracking.Track();

            _lblVersionInfo.Text = $"StatusChecker v{ VersionTracking.CurrentVersion }";
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
            var updatedStatusRequestUrlSetting = new Setting
            {
                Id = (int)SettingKeys.StatusRequestUrl,
                Key = SettingKeys.StatusRequestUrl.ToString(),
                Value = _viewModel.StatusRequestUrl
            };

            await _settingRepository.SaveAsync(updatedStatusRequestUrlSetting);

            Application.Current.MainPage = new MainPage();
        }

        private void Cancel_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }


    }
}
