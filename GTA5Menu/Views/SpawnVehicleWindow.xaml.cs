using GTA5Menu.Data;
using GTA5Menu.Views.SpawnVehicle;

using CommunityToolkit.Mvvm.Input;

namespace GTA5Menu.Views;

/// <summary>
/// SpawnVehicleWindow.xaml 的交互逻辑
/// </summary>
public partial class SpawnVehicleWindow
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public List<NavMenu> NavMenus { get; set; } = new();

    private readonly MyVehicleView MyVehicleView = new();
    private readonly FindVehicleView FindVehicleView = new();
    private readonly AllVehicleView AllVehicleView = new();

    /// <summary>
    /// 主窗口关闭事件
    /// </summary>
    public static event Action WindowClosingEvent;

    public SpawnVehicleWindow()
    {
        InitializeComponent();
    }

    private void Window_SpawnVehicle_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        // 创建菜单
        CreateNavMenus();
        // 设置主页
        ContentControl_Main.Content = MyVehicleView;
    }

    private void Window_SpawnVehicle_Closing(object sender, CancelEventArgs e)
    {
        WindowClosingEvent();
    }

    /// <summary>
    /// 创建导航菜单
    /// </summary>
    private void CreateNavMenus()
    {
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "我的收藏", ViewName = "MyVehicleView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "搜索载具", ViewName = "FindVehicleView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "全部载具", ViewName = "AllVehicleView" });
    }

    /// <summary>
    /// 页面导航
    /// </summary>
    /// <param name="menu"></param>
    [RelayCommand]
    private void Navigate(NavMenu menu)
    {
        if (menu == null || string.IsNullOrEmpty(menu.ViewName))
            return;

        switch (menu.ViewName)
        {
            case "MyVehicleView":
                ContentControl_Main.Content = MyVehicleView;
                break;
            case "FindVehicleView":
                ContentControl_Main.Content = FindVehicleView;
                break;
            case "AllVehicleView":
                ContentControl_Main.Content = AllVehicleView;
                break;
        }
    }
}
