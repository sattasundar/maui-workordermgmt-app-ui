namespace TechAid;

public partial class AppShell : SimpleToolkit.SimpleShell.SimpleShell
{
    public AppShell()
    {
        InitializeComponent();

        AddTab(typeof(WorkOrdersPage), PageType.WorkOrdersPage);
        AddTab(typeof(AssetsPage), PageType.AssetsPage);
        AddTab(typeof(InspectionPage), PageType.InspectionPage);
        AddTab(typeof(SchedulePage), PageType.SchedulePage);
        AddTab(typeof(InventoryPage), PageType.InventoryPage);

        Loaded += AppShellLoaded;
    }
    private static void AppShellLoaded(object sender, EventArgs e)
    {
        var shell = sender as AppShell;

        shell.Window.SubscribeToSafeAreaChanges(safeArea =>
        {
            shell.pageContainer.Margin = safeArea;
            shell.tabBarView.Margin = safeArea;
            shell.bottomBackgroundRectangle.IsVisible = safeArea.Bottom > 0;
            shell.bottomBackgroundRectangle.HeightRequest = safeArea.Bottom;
        });
    }

    private void AddTab(Type page, PageType pageEnum)
    {
        Tab tab = new Tab { Route = pageEnum.ToString(), Title = pageEnum.ToString() };
        tab.Items.Add(new ShellContent { ContentTemplate = new DataTemplate(page) });

        tabBar.Items.Add(tab);
    }

    private void TabBarViewCurrentPageChanged(object sender, TabBarEventArgs e)
    {
        Shell.Current.GoToAsync("///" + e.CurrentPage.ToString());
    }
}
public enum PageType
{
    WorkOrdersPage, AssetsPage, InspectionPage, SchedulePage,  InventoryPage
}
