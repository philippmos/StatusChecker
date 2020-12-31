using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Essentials;
using Xamarin.Forms;

using StatusChecker.Helper.Interfaces;
using StatusChecker.Models.Enums;
using StatusChecker.Services.Interfaces;

namespace StatusChecker.Helper
{
    public static class AppHelper
    {
        /// <summary>
        /// Returns the AppName with AppVersion and AppBuildNumber, if defined
        /// </summary>
        /// <returns></returns>
        public static string GetAppVersionInformation()
        {
            string appBuildNumber = AppSettingsManager.Settings["AppBuildNumber"];
            string appBuildNumberString = "";

            if (!string.IsNullOrEmpty(appBuildNumber)) appBuildNumberString = $"({ appBuildNumber })";

            VersionTracking.Track();

            return $"StatusChecker v{ VersionTracking.CurrentVersion } { appBuildNumberString }";
        }


        /// <summary>
        /// Tracks Errors for Crashes and Exceptions
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="properties"></param>
        public static void TrackError(Exception exception, Dictionary<string, string> properties)
        {
            if (App.PermissionTrackErrors)
            {
                Crashes.TrackError(exception, properties);
            }

            Debug.WriteLine(exception.Message);
        }


        /// <summary>
        /// Parse a given Boolean into Database-ready Settingvalue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ParseBoolSetting(bool value)
        {
            return value ? "1" : "0";
        }


        /// <summary>
        /// Initialize selection of AppTheme at AppStart
        /// </summary>
        public static async void InitializeStyleTheme()
        {
            var settingService = DependencyService.Get<ISettingService>();

            var isDarkModeEnabled = await settingService.GetSettingValueAsync(SettingKeys.DarkModeEnabled);

            Themes activeTheme = Themes.Light;

            if (!string.IsNullOrEmpty(isDarkModeEnabled) && isDarkModeEnabled == "1") activeTheme = Themes.Dark;


            DependencyService.Get<IThemeHelper>().SetAppTheme(activeTheme);
        }
    }
}
