using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;

using StatusChecker.ViewModels;
using StatusChecker.Services.Interfaces;
using StatusChecker.Helper.Interfaces;
using StatusChecker.Helper;
using StatusChecker.Models.Enums;
using StatusChecker.I18N;

namespace StatusChecker.Views
{
    public partial class SettingsPage : ContentPage
    {
        #region Fields
        private SettingsViewModel _viewModel;
        private readonly ISettingService _settingService;
        #endregion


        #region Construction
        public SettingsPage()
        {
            InitializeComponent();

            _settingService = DependencyService.Get<ISettingService>();
           
            _lblVersionInfo.Text = AppHelper.GetAppVersionInformation();
        }
        #endregion


        #region View Handler
        protected override async void OnAppearing()
        {
            _viewModel = new SettingsViewModel()
            {
                Title = AppTranslations.Page_Title_Settings
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

            #region GadgetSortingSetting
            // TODO: Fix this static stuff...
            var allEnumTypes = new List<GadgetSortingTypes>
            {
                GadgetSortingTypes.ByCreationAsc,
                GadgetSortingTypes.ByCreationDesc,
                GadgetSortingTypes.ByNameAsc,
                GadgetSortingTypes.ByNameDesc,
                GadgetSortingTypes.ByLocationAsc,
                GadgetSortingTypes.ByLocationDesc,
                GadgetSortingTypes.ByTemperatureDesc,
                GadgetSortingTypes.ByTemperatureAsc
            };


            var gadgetSortingOptions = (allEnumTypes.Select(enumType => GetGadgetSortingTypeName(enumType))).ToList();

            _pckGadgetSortingType.ItemsSource = gadgetSortingOptions;


            var currentGadgetSorting = await _settingService.GetSettingValueAsync(SettingKeys.GadgetSortingType);

            int.TryParse(currentGadgetSorting, out int gadgetSortingId);
            _pckGadgetSortingType.SelectedIndex = gadgetSortingId - 1;
            #endregion



            #region WebRequestTimeoutSetting
            var timeoutSettingOptions = new List<string>();

            for(int i = 1; i < 20; i++)
            {
                timeoutSettingOptions.Add($"{i} " + AppTranslations.Page_Settings_Setting_Timeout_Measurement);
            }

            _pckTimeoutSetting.ItemsSource = timeoutSettingOptions;


            var requestTimeoutInSeconds = await _settingService.GetSettingValueAsync(SettingKeys.RequestTimeoutInSeconds);

            int.TryParse(requestTimeoutInSeconds, out int requestTimeout);
            _pckTimeoutSetting.SelectedIndex = requestTimeout - 1;
            #endregion

            BindingContext = _viewModel;
        }
        #endregion


        #region View Events
        /// <summary>
        /// Request Updating Setting Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Clicked(object sender, System.EventArgs e)
        {
            var selectedIndex = _pckGadgetSortingType.SelectedIndex;

            _settingService.UpdateSettingsValues(new Dictionary<SettingKeys, string>()
            {
                {
                    SettingKeys.StatusRequestUrl,
                    _viewModel.StatusRequestUrl
                },
                {
                    SettingKeys.GadgetSortingType,
                    (selectedIndex + 1).ToString()
                },
                {
                    SettingKeys.PermissionTrackErrors,
                    AppHelper.ParseBoolSetting(_swtPermissionTrackErrors.IsToggled)
                },
                {
                    SettingKeys.NotifyWhenStatusNotRespond,
                    AppHelper.ParseBoolSetting(_swtNotifyWhenStatusNotRespond.IsToggled)
                },
                {
                    SettingKeys.RequestTimeoutInSeconds,
                    (_pckTimeoutSetting.SelectedIndex + 1).ToString()
                },
                {
                    SettingKeys.DarkModeEnabled,
                    AppHelper.ParseBoolSetting(_swtDarkmodeEnabled.IsToggled)
                }
            });


            Application.Current.MainPage = new MainPage();
        }

        /// <summary>
        /// Cancel Settings View and open MainView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
        
        /// <summary>
        /// Instantly Toggle AppTheme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _swtDarkmodeEnabled_Toggled(object sender, ToggledEventArgs e)
        {
            SetTheme(_swtDarkmodeEnabled.IsToggled);
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// Updates the current selected AppTheme
        /// </summary>
        /// <param name="status"></param>
        private void SetTheme(bool status)
        {
            Themes themeRequested = Themes.Light;

            if (status)
            {
                themeRequested = Themes.Dark;
            }

            DependencyService.Get<IThemeHelper>().SetAppTheme(themeRequested);
        }

        /// <summary>
        /// Static assignment of Sorting-Names
        /// </summary>
        /// <param name="gadgetSortingType"></param>
        /// <returns></returns>
        private string GetGadgetSortingTypeName(GadgetSortingTypes gadgetSortingType)
        {
            string ascText = AppTranslations.Page_Settings_Setting_SortingType_Option_AscText;
            string descText = AppTranslations.Page_Settings_Setting_SortingType_Option_DescText;

            switch (gadgetSortingType)
            {
                case GadgetSortingTypes.ByCreationDesc:
                    return AppTranslations.Page_Settings_Setting_SortingType_Option_ByCreation + " " + descText;

                case GadgetSortingTypes.ByNameAsc:
                    return AppTranslations.Page_Settings_Setting_SortingType_Option_ByName + " " + ascText;

                case GadgetSortingTypes.ByNameDesc:
                    return AppTranslations.Page_Settings_Setting_SortingType_Option_ByName + " " + descText;

                case GadgetSortingTypes.ByLocationAsc:
                    return AppTranslations.Page_Settings_Setting_SortingType_Option_ByLocation + " " + ascText;

                case GadgetSortingTypes.ByLocationDesc:
                    return AppTranslations.Page_Settings_Setting_SortingType_Option_ByLocation + " " + descText;

                case GadgetSortingTypes.ByTemperatureAsc:
                    return AppTranslations.Page_Settings_Setting_SortingType_Option_ByTemperature + " " + ascText;

                case GadgetSortingTypes.ByTemperatureDesc:
                    return AppTranslations.Page_Settings_Setting_SortingType_Option_ByTemperature + " " + descText;

                case GadgetSortingTypes.ByCreationAsc:
                default:
                    return AppTranslations.Page_Settings_Setting_SortingType_Option_ByCreation + " " + ascText;
            }
        }
        #endregion
    }
}
