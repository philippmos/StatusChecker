using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Forms;

using StatusChecker.Services;
using StatusChecker.Infrastructure.Repositories;
using StatusChecker.DataStore;

namespace StatusChecker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<GadgetDataStore>();

            DependencyService.Register<WebRequestService>();

            DependencyService.Register<GadgetRepository>();
            DependencyService.Register<SettingRepository>();


            MainPage = new Views.MainPage();
        }

        protected override void OnStart()
        {
            var appCenterSecretForms = AppSettingsManager.Settings["AppCenterSecretForms"];

            if(!string.IsNullOrEmpty(appCenterSecretForms))
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
