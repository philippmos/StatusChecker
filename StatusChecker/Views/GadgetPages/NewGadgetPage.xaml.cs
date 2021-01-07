using System;
using System.Linq;

using Xamarin.Forms;

using StatusChecker.Models.Database;
using StatusChecker.Helper;
using StatusChecker.I18N;

namespace StatusChecker.Views.GadgetPages
{
    public partial class NewGadgetPage : ContentPage
    {
        #region Fields
        public Gadget Gadget { get; set; }
        #endregion


        #region Construction
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
        #endregion


        #region View Events
        /// <summary>
        /// Validates Input and saves new Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            var validationErrorList = ValidationHelper.CreateValidationErrorList(Gadget);

            if(validationErrorList.Count() == 0)
            {
                MessagingCenter.Send(this, "AddItem", Gadget);

                Application.Current.MainPage = new MainPage();
            }

            var errorString = string.Join(", ", validationErrorList);

            await DisplayAlert(AppTranslations.Page_NewGadget_Validation_Alert_Title, errorString, AppTranslations.Main_Button_Title_Ok);
        }

        /// <summary>
        /// Cancel the current Input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        #endregion
    }
}