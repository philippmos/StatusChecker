using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Forms;

using StatusChecker.Views.LegalPages;
using StatusChecker.Views.GadgetPages;
using StatusChecker.Models.Enums;

namespace StatusChecker.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        #region Fields
        private readonly Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        #endregion


        #region Construction
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemTypes.GadgetOverview, (NavigationPage)Detail);
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// Handling / Routing Menu-Navigation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemTypes.GadgetOverview:
                        MenuPages.Add(id, new NavigationPage(new GadgetsPage()));
                        break;
                    case (int)MenuItemTypes.Setting:
                        MenuPages.Add(id, new NavigationPage(new SettingsPage()));
                        break;
                    case (int)MenuItemTypes.AppInfo:
                        MenuPages.Add(id, new NavigationPage(new AppInfoPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
        #endregion
    }
}