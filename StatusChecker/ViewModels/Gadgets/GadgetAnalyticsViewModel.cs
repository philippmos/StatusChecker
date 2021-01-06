namespace StatusChecker.ViewModels.Gadgets
{
    public class GadgetAnalyticsViewModel : BaseViewModel
    {
        public string AmountOfEntries { get; set; }
        public string TemperatureAvg { get; set; }
        public string TemperatureMaxAndDate { get; set; }        
        public string TemperatureMinAndDate { get; set; }


        private readonly string notAvailableInitialInfo = "nicht verfügbar";

        public GadgetAnalyticsViewModel()
        {
            AmountOfEntries = notAvailableInitialInfo;
            TemperatureAvg = notAvailableInitialInfo;
            TemperatureMaxAndDate = notAvailableInitialInfo;
            TemperatureMinAndDate = notAvailableInitialInfo;
        }
    }
}
