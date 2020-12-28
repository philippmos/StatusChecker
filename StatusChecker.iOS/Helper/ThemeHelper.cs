using Xamarin.Forms;

using StatusChecker.Helper.Interfaces;
using StatusChecker.StyleThemes;
using static StatusChecker.App;

[assembly: Dependency(typeof(StatusChecker.iOS.Helper.ThemeHelper))]
namespace StatusChecker.iOS.Helper
{
    public class ThemeHelper : IThemeHelper
    {
        public void SetAppTheme(App.Theme theme)
        {
            SetTheme(theme);
        }


        void SetTheme(Theme mode)
        {

            if (mode == Theme.Dark)
            {
                if (App.AppTheme == Theme.Dark) return;

                App.Current.Resources = new DarkTheme();
            }
            else
            {
                if (App.AppTheme != Theme.Dark) return;
                
                App.Current.Resources = new LightTheme();
            }

            App.AppTheme = mode;
        }

    }
}
