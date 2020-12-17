using Xamarin.Forms;

using StatusChecker.Services;

namespace StatusChecker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<GadgetDataStore>();

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
