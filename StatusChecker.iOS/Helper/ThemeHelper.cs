using Xamarin.Forms;

using StatusChecker.Helper.Interfaces;
using StatusChecker.StyleThemes;
using StatusChecker.Models.Enums;

[assembly: Dependency(typeof(StatusChecker.iOS.Helper.ThemeHelper))]
namespace StatusChecker.iOS.Helper
{
    public class ThemeHelper : IThemeHelper
    {
        public void SetAppTheme(Themes theme)
        {
            SetTheme(theme);
        }


        void SetTheme(Themes mode)
        {

            if (mode == Themes.Dark)
            {
                if (App.AppTheme == Themes.Dark) return;

                App.Current.Resources = new DarkTheme();
            }
            else
            {
                if (App.AppTheme != Themes.Dark) return;
                
                App.Current.Resources = new LightTheme();
            }

            App.AppTheme = mode;
        }

    }
}
