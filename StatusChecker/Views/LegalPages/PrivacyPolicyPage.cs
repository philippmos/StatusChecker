using Xamarin.Forms;

namespace StatusChecker.Views.LegalPages
{
    public class PrivacyPolicyPage : ContentPage
    {
        #region Construction
        public PrivacyPolicyPage()
        {
            var browser = new WebView();

            browser.Source = AppSettingsManager.Settings["LegalPrivacyPolicyUrl"];
            Content = browser;
        }
        #endregion
    }
}
