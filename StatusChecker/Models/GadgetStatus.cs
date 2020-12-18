namespace StatusChecker.Models
{
    public class GadgetStatus
    {
        public double temperature { get; set; }
        public bool overtemperature { get; set; }
        public string temperature_status { get; set; }
        public string mac { get; set; }
    }
}
