using Xamarin.Forms;

namespace StatusChecker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void CheckupButton_Clicked(System.Object sender, System.EventArgs e)
        {
            _temperature_248.Text = "Works!";
        }
    }
}
