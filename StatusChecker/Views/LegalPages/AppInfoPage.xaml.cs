using Xamarin.Forms;

using StatusChecker.Helper;

namespace StatusChecker.Views.LegalPages
{
    public partial class AppInfoPage : ContentPage
    {
        #region Construction
        public AppInfoPage()
        {
            InitializeComponent();

            _lblVersionInfo.Text = AppHelper.GetAppVersionInformation();
        }
        #endregion


        #region View Events
        /// <summary>
        /// Opens the Imprint WebView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void _btnImprint_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ImprintPage { Title = "Impressum" });
        }

        /// <summary>
        /// Opens the PrivacyPolicy WebView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void _btnPrivacyPolicy_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PrivacyPolicyPage { Title = "Datenschutz" });
        }
        #endregion
    }
}
