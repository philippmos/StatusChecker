using SQLite;

namespace StatusChecker.Models.Database
{
    public class Setting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public enum SettingKeys
    {
        StatusRequestUrl = 1,
        PermissionTrackErrors = 2,
        NotifyWhenStatusNotRespond = 3,
        RequestTimeoutInSeconds = 4,
        DarkModeEnabled = 5
    }
}
