namespace StatusChecker.Models.Database
{
    public class Setting : DbBaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
