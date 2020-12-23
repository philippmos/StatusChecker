using System;

using Xamarin.Forms;

using StatusChecker.Models.Database;

namespace StatusChecker.Views.GadgetPages
{
    public partial class NewGadgetPage : ContentPage
    {
        public Gadget Gadget { get; set; }

        public NewGadgetPage()
        {
            InitializeComponent();

            Gadget = new Gadget
            {
                Name = "",
                Location = "",
                IpAddress = "",
                Description = ""
            };

            BindingContext = this;
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Gadget);

            Application.Current.MainPage = new MainPage();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}