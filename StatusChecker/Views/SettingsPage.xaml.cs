using Xamarin.Essentials;
using Xamarin.Forms;

using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;
using StatusChecker.ViewModels;
using StatusChecker.Services.Interfaces;
using System.Collections.Generic;
using StatusChecker.Helper.Interfaces;
using static StatusChecker.App;

namespace StatusChecker.Views
{
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel _viewModel;
        private readonly IRepository<Setting> _settingRepository;
        private readonly ISettingService _settingService;


        public SettingsPage()
        {
            InitializeComponent();

            _settingRepository = DependencyService.Get<IRepository<Setting>>();
            _settingService = DependencyService.Get<ISettingService>();

            VersionTracking.Track();

            _lblVersionInfo.Text = $"StatusChecker v{ VersionTracking.CurrentVersion }";
        }


        protected override async void OnAppearing()
        {
            _viewModel = new SettingsViewModel()
            {
                Title = "Einstellungen"
            };

            var permissionTrackErrors = await _settingService.GetSettingValueAsync(SettingKeys.PermissionTrackErrors);
            var notifyWhenStatusNotRespond = await _settingService.GetSettingValueAsync(SettingKeys.NotifyWhenStatusNotRespond);
            var darkModeEnabled = await _settingService.GetSettingValueAsync(SettingKeys.DarkModeEnabled);


            _viewModel.StatusRequestUrl = await _settingService.GetSettingValueAsync(SettingKeys.StatusRequestUrl); ;
 
            if(permissionTrackErrors == "1")
            {
                _viewModel.PermissionTrackErrors = true;
            }
            if(notifyWhenStatusNotRespond == "1")
            {
                _viewModel.NotifyWhenStatusNotRespond = true;
            }
            if(darkModeEnabled == "1")
            {
                _viewModel.DarkModeEnabled = true;
            }

            var timeoutSettingOptions = new List<string>();

            for(int i = 1; i < 20; i++)
            {
                timeoutSettingOptions.Add($"{i} Sekunden");
            }

            _pckTimeoutSetting.ItemsSource = timeoutSettingOptions;


            var requestTimeoutInSeconds = await _settingService.GetSettingValueAsync(SettingKeys.RequestTimeoutInSeconds);

            int.TryParse(requestTimeoutInSeconds, out int requestTimeout);
            _pckTimeoutSetting.SelectedIndex = requestTimeout - 1;
            

            BindingContext = _viewModel;
        }


        private void Save_Clicked(object sender, System.EventArgs e)
        {
            _settingService.UpdateSettingsValues(new Dictionary<SettingKeys, string>()
            {
                {
                    SettingKeys.StatusRequestUrl,
                    _viewModel.StatusRequestUrl
                },
                {
                    SettingKeys.PermissionTrackErrors,
                    ParseBoolSetting(_swtPermissionTrackErrors.IsToggled)
                },
                {
                    SettingKeys.NotifyWhenStatusNotRespond,
                    ParseBoolSetting(_swtNotifyWhenStatusNotRespond.IsToggled)
                },
                {
                    SettingKeys.RequestTimeoutInSeconds,
                    (_pckTimeoutSetting.SelectedIndex + 1).ToString()
                },
                {
                    SettingKeys.DarkModeEnabled,
                    ParseBoolSetting(_swtDarkmodeEnabled.IsToggled)
                }
            });


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

        private void _swtDarkmodeEnabled_Toggled(object sender, ToggledEventArgs e)
        {
            SetTheme(_swtDarkmodeEnabled.IsToggled);
        }

        private void SetTheme(bool status)
        {
            Theme themeRequested = Theme.Light;

            if (status)
            {
                themeRequested = Theme.Dark;
            }

            DependencyService.Get<IThemeHelper>().SetAppTheme(themeRequested);
        }

    }
}
