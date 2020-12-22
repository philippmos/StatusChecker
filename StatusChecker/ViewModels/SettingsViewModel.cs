namespace StatusChecker.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public string StatusRequestUrl { get; set; }
        public bool PermissionTrackErrors { get; set; }
        public bool NotifyWhenStatusNotRespond { get; set; }
    }
}
