using Xamarin.Forms;

using StatusChecker.Services;
using StatusChecker.Infrastructure;

namespace StatusChecker
{
    public partial class App : Application
    {
        static StatusCheckerDatabase database;

        public static StatusCheckerDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new StatusCheckerDatabase();
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            DependencyService.Register<GadgetDataStore>();
            DependencyService.Register<WebRequestService>();

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
