using StatusChecker.Models;

namespace StatusChecker.ViewModels
{
    public class GadgetDetailViewModel : BaseViewModel
    {
        public Gadget Gadget { get; set; }
        public GadgetDetailViewModel(Gadget gadget = null)
        {
            Title = gadget?.Name;
            Gadget = gadget;
        }
    }
}
