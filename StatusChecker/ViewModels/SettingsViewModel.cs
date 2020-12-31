namespace StatusChecker.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public string StatusRequestUrl { get; set; }
        public bool PermissionTrackErrors { get; set; }
        public bool NotifyWhenStatusNotRespond { get; set; }
        public int RequestTimeoutInSeconds { get; set; }
        public bool DarkModeEnabled { get; set; }
        public int GadgetSortingType { get; set; }
    }
}
