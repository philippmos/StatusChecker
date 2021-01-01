namespace StatusChecker.Models.Database
{
    public class Gadget : DbBaseModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
    }
}