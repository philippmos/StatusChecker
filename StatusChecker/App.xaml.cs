using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Forms;
using Xamarin.Essentials;

using StatusChecker.Services;
using StatusChecker.Infrastructure.Repositories;
using StatusChecker.DataStore;
using StatusChecker.Models.Database;
using StatusChecker.Services.Interfaces;
using StatusChecker.Helper.Interfaces;

namespace StatusChecker
{
    public partial class App : Application
    {
        public static bool PermissionTrackErrors = false;
        public static Theme AppTheme { get; set; }

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

        /// <summary>
        /// Tracks Errors for Crashes and Exceptions
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="properties"></param>
        public static void TrackError(Exception exception, Dictionary<string, string> properties)
        {
            if (PermissionTrackErrors)
            {
                Crashes.TrackError(exception, properties);
            }

            Debug.WriteLine(exception.Message);
        }

        

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


        public enum Theme
        {
            Light,
            Dark
        }


        public static string GetAppVersionInformation()
        {
            string appBuildNumber = AppSettingsManager.Settings["AppBuildNumber"];
            string appBuildNumberString = "";

            if (!string.IsNullOrEmpty(appBuildNumber)) appBuildNumberString = $"({ appBuildNumber })";

            VersionTracking.Track();

            return $"StatusChecker v{ VersionTracking.CurrentVersion } { appBuildNumberString }";
        }


        private async void InitializeStyleTheme()
        {
            var settingService = DependencyService.Get<ISettingService>();

            var isDarkModeEnabled = await settingService.GetSettingValueAsync(SettingKeys.DarkModeEnabled);

            Theme activeTheme = Theme.Light;

            if (!string.IsNullOrEmpty(isDarkModeEnabled) && isDarkModeEnabled == "1") activeTheme = Theme.Dark;


            DependencyService.Get<IThemeHelper>().SetAppTheme(activeTheme);
        }

    }
}
