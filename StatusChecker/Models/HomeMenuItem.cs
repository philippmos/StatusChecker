namespace StatusChecker.Models
{
    public enum MenuItemType
    {
        GadgetOverview,
        AppInfo,
        Setting
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
