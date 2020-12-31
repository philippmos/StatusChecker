using StatusChecker.Models.Enums;

namespace StatusChecker.Helper.Interfaces
{
    public interface IThemeHelper
    {
        /// <summary>
        /// Update the current active AppTheme InApp
        /// </summary>
        /// <param name="theme"></param>
        void SetAppTheme(Themes theme);
    }
}
