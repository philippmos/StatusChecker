using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Forms;

using StatusChecker.Services;
using StatusChecker.Infrastructure.Repositories;
using StatusChecker.DataStore;
using StatusChecker.Services.Interfaces;
using StatusChecker.Helper.Interfaces;
using StatusChecker.Models.Enums;

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

            DependencyService.Register<WebRequestService>();
            DependencyService.Register<SettingService>();

            DependencyService.Register<GadgetRepository>();
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

            InitializeStyleTheme();

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        #endregion


        private async void InitializeStyleTheme()
        {
            var settingService = DependencyService.Get<ISettingService>();

            var isDarkModeEnabled = await settingService.GetSettingValueAsync(SettingKeys.DarkModeEnabled);

            Themes activeTheme = Themes.Light;

            if (!string.IsNullOrEmpty(isDarkModeEnabled) && isDarkModeEnabled == "1") activeTheme = Themes.Dark;


            DependencyService.Get<IThemeHelper>().SetAppTheme(activeTheme);
        }

    }
}
