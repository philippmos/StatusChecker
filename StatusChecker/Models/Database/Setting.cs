namespace StatusChecker.Models.Database
{
    public class Setting : BaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
