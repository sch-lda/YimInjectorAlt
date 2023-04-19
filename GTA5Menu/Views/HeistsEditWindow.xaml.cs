﻿using GTA5Menu.Data;
using GTA5Menu.Views.HeistsEdit;

using CommunityToolkit.Mvvm.Input;

namespace GTA5Menu.Views;

/// <summary>
/// HeistsEditWindow.xaml 的交互逻辑
/// </summary>
public partial class HeistsEditWindow
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public List<NavMenu> NavMenus { get; set; } = new();

    private readonly ReadMeView ReadMeView = new();
    private readonly ContractView ContractView = new();
    private readonly PericoView PericoView = new();
    private readonly CasinoView CasinoView = new();
    private readonly DoomsdayView DoomsdayView = new();
    private readonly ApartmentView ApartmentView = new();

    ///////////////////////////////////////////////////////////////

    public HeistsEditWindow()
    {
        InitializeComponent();
    }

    private void Window_HeistPreps_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        // 创建菜单
        CreateMenuBar();
        // 设置主页
        ContentControl_Main.Content = ReadMeView;

        ///////////////////////////////////////////////////////////////;
    }

    private void Window_HeistPreps_Closing(object sender, CancelEventArgs e)
    {

    }

    /// <summary>
    /// 创建导航菜单
    /// </summary>
    private void CreateMenuBar()
    {
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "使用说明", ViewName = "ReadMeView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "事所合约", ViewName = "ContractView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "佩里克岛", ViewName = "PericoView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "赌场抢劫", ViewName = "CasinoView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "末日抢劫", ViewName = "DoomsdayView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "公寓抢劫", ViewName = "ApartmentView" });
    }

    /// <summary>
    /// 页面导航（重复点击不会重复触发）
    /// </summary>
    /// <param name="menu"></param>
    [RelayCommand]
    private void Navigate(NavMenu menu)
    {
        if (menu == null || string.IsNullOrEmpty(menu.ViewName))
            return;

        switch (menu.ViewName)
        {
            case "ReadMeView":
                ContentControl_Main.Content = ReadMeView;
                break;
            case "ContractView":
                ContentControl_Main.Content = ContractView;
                break;
            case "PericoView":
                ContentControl_Main.Content = PericoView;
                break;
            case "CasinoView":
                ContentControl_Main.Content = CasinoView;
                break;
            case "DoomsdayView":
                ContentControl_Main.Content = DoomsdayView;
                break;
            case "ApartmentView":
                ContentControl_Main.Content = ApartmentView;
                break;
        }
    }
}
