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
            var permissionTrackErrors = await _settingRepository.GetAsync((int)SettingKeys.PermissionTrackErrors);
            var notifyWhenStatusNotRespond = await _settingRepository.GetAsync((int)SettingKeys.NotifyWhenStatusNotRespond);

            if (statusRequestUrl != null)
            {
                _viewModel.StatusRequestUrl = statusRequestUrl.Value;
            }
            if (permissionTrackErrors != null && permissionTrackErrors.Value == "1")
            {
                _swtPermissionTrackErrors.IsToggled = true;
            }
            if (notifyWhenStatusNotRespond != null && notifyWhenStatusNotRespond.Value == "1")
            {
                _viewModel.NotifyWhenStatusNotRespond = true;
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


            var updatedPermissionTrackErrors = new Setting
            {
                Id = (int)SettingKeys.PermissionTrackErrors,
                Key = SettingKeys.PermissionTrackErrors.ToString(),
                Value = ParseBoolSetting(_swtPermissionTrackErrors.IsToggled)
            };

            await _settingRepository.SaveAsync(updatedPermissionTrackErrors);


            var updatedNotifyWhenStatusNotRespond = new Setting
            {
                Id = (int)SettingKeys.NotifyWhenStatusNotRespond,
                Key = SettingKeys.NotifyWhenStatusNotRespond.ToString(),
                Value = ParseBoolSetting(_swtNotifyWhenStatusNotRespond.IsToggled)
            };

            await _settingRepository.SaveAsync(updatedNotifyWhenStatusNotRespond);



            Application.Current.MainPage = new MainPage();
        }

        private void Cancel_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }


        private string ParseBoolSetting(bool value)
        {
            return value ? "1" : "0";
        }


    }
}
