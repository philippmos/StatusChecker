using Xamarin.Essentials;
using Xamarin.Forms;

using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;
using StatusChecker.ViewModels;
using StatusChecker.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

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


            _viewModel.StatusRequestUrl = await _settingService.GetSettingValueAsync(SettingKeys.StatusRequestUrl); ;
 
            if(permissionTrackErrors == "1")
            {
                _swtPermissionTrackErrors.IsToggled = true;
            }
            if(notifyWhenStatusNotRespond == "1")
            {
                _viewModel.NotifyWhenStatusNotRespond = true;
            }

            _pckTimeoutSetting.ItemsSource = Enumerable.Range(1, 20).ToList();
            

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
    }
}
