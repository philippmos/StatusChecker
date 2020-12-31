using Xamarin.Forms;

namespace StatusChecker.Views.LegalPages
{
    public class ImprintPage : ContentPage
    {
        public ImprintPage()
        {
            var browser = new WebView();

            browser.Source = AppSettingsManager.Settings["LegalImprintUrl"];
            Content = browser;
        }
    }
}
