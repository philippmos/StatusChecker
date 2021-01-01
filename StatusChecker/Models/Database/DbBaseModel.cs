using SQLite;

namespace StatusChecker.Models.Database
{
    public class DbBaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
