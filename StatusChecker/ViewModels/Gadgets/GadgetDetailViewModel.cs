namespace StatusChecker.ViewModels.Gadgets
{
    public class GadgetDetailViewModel : BaseViewModel
    {
        public GadgetViewModel Gadget { get; set; }
        public GadgetAnalyticsViewModel GadgetAnalytics { get; set; }

        public GadgetDetailViewModel(GadgetViewModel gadget = null, GadgetAnalyticsViewModel gadgetAnalytics = null)
        {
            Title = gadget?.Name;
            Gadget = gadget;
            GadgetAnalytics = GadgetAnalytics;
        }
    }
}
