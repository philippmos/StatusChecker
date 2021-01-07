using System.Collections.Generic;

using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.Models.Enums;
using StatusChecker.I18N;

namespace StatusChecker.Views
{
    public partial class MenuPage : ContentPage
    {
        #region Fields
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        private readonly List<HomeMenuItem> menuItems;
        #endregion


        #region Construction
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem { Id = MenuItemTypes.GadgetOverview, Title = AppTranslations.Main_Navigation_Entries_Gadgets },
                new HomeMenuItem { Id = MenuItemTypes.Setting, Title = AppTranslations.Main_Navigation_Entries_Settings },
                new HomeMenuItem { Id = MenuItemTypes.AppInfo, Title = AppTranslations.Main_Navigation_Entries_Information }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null) return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;

                await RootPage.NavigateFromMenu(id);
            };
        }
        #endregion
    }
}