namespace StatusChecker.ViewModels
{
    public class GadgetViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
        public bool IsStatusOk { get; set; }
        public string TemperatureStatus { get; set; }
        public double Temperature { get; set; }
        public string TemperatureC { get; set; }
    }
}
