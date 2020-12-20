namespace StatusChecker.Models
{
    public enum MenuItemType
    {
        Browse,
        AppInfo
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
