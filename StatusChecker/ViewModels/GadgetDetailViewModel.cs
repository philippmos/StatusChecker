using StatusChecker.Models;

namespace StatusChecker.ViewModels
{
    public class GadgetDetailViewModel : BaseViewModel
    {
        public GadgetViewModel Gadget { get; set; }

        public GadgetDetailViewModel(GadgetViewModel gadget = null)
        {
            Title = gadget?.Name;
            Gadget = gadget;
        }
    }
}
