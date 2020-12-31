using SQLite;

namespace StatusChecker.Models.Database
{
    public class BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
