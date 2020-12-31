using Xamarin.Forms;

namespace StatusChecker.Views.LegalPages
{
    public class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            var browser = new WebView();

            browser.Source = AppSettingsManager.Settings["LegalPrivacyPolicyUrl"];
            Content = browser;
        }
    }
}

