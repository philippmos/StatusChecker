using Xamarin.Forms;

using StatusChecker.Services;
using StatusChecker.Infrastructure.Repositories;
using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;

namespace StatusChecker
{
    public partial class App : Application
    {
        static IDatabase<Gadget> database;

        public static IDatabase<Gadget> Database
        {
            get
            {
                if (database == null)
                {
                    database = DependencyService.Get<IDatabase<Gadget>>(); ;
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            DependencyService.Register<GadgetDataStore>();
            DependencyService.Register<WebRequestService>();
            DependencyService.Register<GadgetDatabase>();

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
