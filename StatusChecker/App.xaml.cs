using Xamarin.Forms;

using StatusChecker.Services;
using StatusChecker.Infrastructure.Repositories;
using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;

namespace StatusChecker
{
    public partial class App : Application
    {
        static IRepository<Gadget> database;

        public static IRepository<Gadget> Database
        {
            get
            {
                if (database == null)
                {
                    database = DependencyService.Get<IRepository<Gadget>>(); ;
                }
                return database;
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
