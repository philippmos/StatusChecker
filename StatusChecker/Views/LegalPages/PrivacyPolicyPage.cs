using Xamarin.Forms;

namespace StatusChecker.Views.LegalPages
{
    public class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            var browser = new WebView();
            var htmlSource = new HtmlWebViewSource();

            htmlSource.Html = @"<html><body>
                                <h1>Datenschutz</h1>
                                <p></p>
                                </body>
                                </html>";

            browser.Source = htmlSource;
            Content = browser;
        }
    }
}

