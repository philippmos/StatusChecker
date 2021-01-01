using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Forms;

using StatusChecker.Services;
using StatusChecker.Infrastructure.Repositories;
using StatusChecker.DataStore;
using StatusChecker.Services.Interfaces;
using StatusChecker.Models.Enums;
using StatusChecker.Helper;

namespace StatusChecker
{
    public partial class App : Application
    {
        #region Fields
        public static bool PermissionTrackErrors = false;
        public static Themes AppTheme { get; set; }
        #endregion


        #region Construction
        public App()
        {
            InitializeComponent();

            #region DependencyService
            DependencyService.Register<GadgetDataStore>();
            DependencyService.Register<SettingDataStore>();

            DependencyService.Register<WebRequestService>();
            DependencyService.Register<GadgetStatusRequestService>();
            DependencyService.Register<SettingService>();

            DependencyService.Register<GadgetRepository>();
            DependencyService.Register<GadgetStatusRequestRepository>();
            DependencyService.Register<SettingRepository>();
            #endregion


            MainPage = new Views.MainPage();
        }
        #endregion



        #region View Handler
        protected override async void OnStart()
        {
            var settingService = DependencyService.Get<ISettingService>();
            var permissionTrackErrorSetting = await settingService.GetSettingValueAsync(SettingKeys.PermissionTrackErrors);

            if(permissionTrackErrorSetting == "1")
            {
                PermissionTrackErrors = true;
            }

            var appCenterSecretForms = AppSettingsManager.Settings["AppCenterSecretForms"];

            if(PermissionTrackErrors && !string.IsNullOrEmpty(appCenterSecretForms))
            {
                AppCenter.Start(appCenterSecretForms,
                                typeof(Analytics),
                                typeof(Crashes));
            }

            AppHelper.InitializeStyleTheme();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        #endregion
    }
}
