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

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if(IsGadgetValidToSave(Gadget))
            {
                MessagingCenter.Send(this, "AddItem", Gadget);

                Application.Current.MainPage = new MainPage();
            }

            await DisplayAlert("Der Eintrag konnte nicht gespeichert werden", "Bitte überprüfe Deine Eingaben", "Ok");
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        private bool IsGadgetValidToSave(Gadget gadget)
        {
            return false;
        }
    }
}