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

            MainPage = new Views.MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
