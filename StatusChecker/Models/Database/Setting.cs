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

    }
}
