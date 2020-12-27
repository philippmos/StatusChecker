using Xamarin.Forms;

namespace StatusChecker.Views.LegalPages
{
    public class ImprintPage : ContentPage
    {
        public ImprintPage()
        {
            var browser = new WebView();
            var htmlSource = new HtmlWebViewSource();

            htmlSource.Html = @"<html><body>
                                <h1>Impressum</h1>
                                <p></p>
                                </body>
                                </html>";

            browser.Source = htmlSource;
            Content = browser;
        }
    }
}

