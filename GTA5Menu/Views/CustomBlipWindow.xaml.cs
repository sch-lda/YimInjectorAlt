using GTA5Menu.Data;
using GTA5Menu.Views.CustonBlip;

using CommunityToolkit.Mvvm.Input;

namespace GTA5Menu.Views;

/// <summary>
/// CustomBlipWindow.xaml 的交互逻辑
/// </summary>
public partial class CustomBlipWindow
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public List<NavMenu> NavMenus { get; set; } = new();

    private readonly MyBlipView MyBlipView = new();
    private readonly AllBlipView AllBlipView = new();

    /// <summary>
    /// 主窗口关闭事件
    /// </summary>
    public static event Action WindowClosingEvent;

    public CustomBlipWindow()
    {
        InitializeComponent();
    }

    private void Window_CustonBlip_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        // 创建菜单
        CreateNavMenus();
        // 设置主页
        ContentControl_Main.Content = MyBlipView;
    }

    private void Window_CustonBlip_Closing(object sender, CancelEventArgs e)
    {
        WindowClosingEvent();
    }

    /// <summary>
    /// 创建导航菜单
    /// </summary>
    private void CreateNavMenus()
    {
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "我的收藏", ViewName = "MyBlipView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "全部Blip", ViewName = "AllBlipView" });
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
            case "MyBlipView":
                ContentControl_Main.Content = MyBlipView;
                break;
            case "AllBlipView":
                ContentControl_Main.Content = AllBlipView;
                break;
        }
    }
}
