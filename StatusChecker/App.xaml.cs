using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Forms;

using StatusChecker.Services;
using StatusChecker.Infrastructure.Repositories;
using StatusChecker.DataStore;
using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;

namespace StatusChecker
{
    public partial class App : Application
    {
        public static bool PermissionTrackErrors = false;

        public App()
        {
            InitializeComponent();

            #region DependencyService
            DependencyService.Register<GadgetDataStore>();

            DependencyService.Register<WebRequestService>();

            DependencyService.Register<GadgetRepository>();
            DependencyService.Register<SettingRepository>();
            #endregion


            MainPage = new Views.MainPage();
        }

        protected async override void OnStart()
        {
            var settingRepository = DependencyService.Get<IRepository<Setting>>();
            var permissionTrackErrorsSetting = await settingRepository.GetAsync((int)SettingKeys.PermissionTrackErrors);

            if(permissionTrackErrorsSetting != null && permissionTrackErrorsSetting.Value == "1")
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

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
