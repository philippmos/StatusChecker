using System;
using System.Collections.Generic;
using System.Linq;

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
            var validationErrorList = CreateValidationErrorList(Gadget);

            if(validationErrorList.Count() == 0)
            {
                MessagingCenter.Send(this, "AddItem", Gadget);

                Application.Current.MainPage = new MainPage();
            }

            var errorString = string.Join(",", validationErrorList);

            await DisplayAlert("Bitte überprüfe Deine Eingaben", errorString, "Ok");
        }


        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        private List<string> CreateValidationErrorList(Gadget gadget)
        {
            var invalidFieldsList = new List<string>();

            if (gadget.Name.Length <= 3)
            {
                invalidFieldsList.Add("Länge des Gerätenamens");
            }

            if(string.IsNullOrEmpty(gadget.IpAddress))
            {
                invalidFieldsList.Add("IP Adresse ist erforderlich");
            }

            return invalidFieldsList;
        }
    }
}