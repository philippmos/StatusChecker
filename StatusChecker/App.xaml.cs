using Xamarin.Forms;

using StatusChecker.Services;
using StatusChecker.Infrastructure;

namespace StatusChecker
{
    public partial class App : Application
    {
        static IDatabase database;

        public static IDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = DependencyService.Get<IDatabase>(); ;
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            DependencyService.Register<GadgetDataStore>();
            DependencyService.Register<WebRequestService>();
            DependencyService.Register<StatusCheckerDatabase>();

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
