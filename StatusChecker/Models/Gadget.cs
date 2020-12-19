using SQLite;

namespace StatusChecker.Models
{
    public class Gadget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
    }
}