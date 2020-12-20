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
        static IRepository<Gadget> gadgetRepository;

        public static IRepository<Gadget> GadgetRepository
        {
            get
            {
                if (gadgetRepository == null)
                {
                    gadgetRepository = DependencyService.Get<IRepository<Gadget>>(); ;
                }
                return gadgetRepository;
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
