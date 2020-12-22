using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Forms;

using StatusChecker.Services;
using StatusChecker.Infrastructure.Repositories;
using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;
using StatusChecker.DataStore;

namespace StatusChecker
{
    public partial class App : Application
    {
        private static IRepository<Gadget> _gadgetRepository;

        public static IRepository<Gadget> GadgetRepository
        {
            get
            {
                if (_gadgetRepository == null)
                {
                    _gadgetRepository = DependencyService.Get<IRepository<Gadget>>(); ;
                }
                return _gadgetRepository;
            }
        }


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
            AppCenter.Start(AppSettingsManager.Settings["AppCenterSecretForms"],
                typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
