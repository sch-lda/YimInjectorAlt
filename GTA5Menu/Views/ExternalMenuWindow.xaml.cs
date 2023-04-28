using GTA5Menu.Data;
using GTA5Menu.Views.ExternalMenu;

using GTA5HotKey;
using GTA5Core.Native;
using GTA5Core.Feature;
using GTA5Core.RAGE.Rage;

using CommunityToolkit.Mvvm.Input;

namespace GTA5Menu.Views;

/// <summary>
/// ExternalMenuWindow.xaml 的交互逻辑
/// </summary>
public partial class ExternalMenuWindow
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public List<NavMenu> NavMenus { get; set; } = new();

    private readonly SelfStateView SelfStateView = new();
    private readonly WorldFunctionView WorldFunctionView = new();
    private readonly OnlineOptionView OnlineOptionView = new();
    private readonly SpawnVehicleView SpawnVehicleView = new();
    private readonly SpawnWeaponView SpawnWeaponView = new();
    private readonly CustomTeleportView CustomTeleportView = new();
    private readonly DriverButlerView DriverButlerView = new();
    private readonly BlipTeleportView BlipTeleportView = new();
    private readonly JobHelperView JobHelperView = new();
    private readonly PlayerListView PlayerListView = new();

    private readonly ReadMeView ReadMeView = new();

    ///////////////////////////////////////////////////////////////

    /// <summary>
    /// 主窗口关闭事件
    /// </summary>
    public static event Action WindowClosingEvent;

    /// <summary>
    /// 主窗口 鼠标坐标数据
    /// </summary>
    private POINT ThisWinPOINT;

    /// <summary>
    /// 是否线上外置窗口菜单
    /// </summary>
    private bool IsShowExternalMenu = true;

    /// <summary>
    /// 判断程序是否在运行，用于结束线程
    /// </summary>
    private bool IsAppRunning = true;

    ///////////////////////////////////////////////////////////////

    public ExternalMenuWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 外置菜单窗口加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_ExternalMenu_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        // 创建菜单
        CreateNavMenus();
        // 设置主页
        ContentControl_Main.Content = SelfStateView;

        ///////////////////////////////////////////////////////////////;

        HotKeys.AddKey(WinVK.DELETE);
        HotKeys.KeyDownEvent += HotKeys_KeyDownEvent;

        new Thread(CPedThread)
        {
            Name = "CPedThread",
            IsBackground = true
        }.Start();

        new Thread(CommonThread)
        {
            Name = "CommonThread",
            IsBackground = true
        }.Start();
    }

    /// <summary>
    /// 外置菜单窗口关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_ExternalMenu_Closing(object sender, CancelEventArgs e)
    {
        WindowClosingEvent();

        IsAppRunning = false;

        Setting.Player.Reset();
        Setting.Vehicle.Reset();
        Setting.Weapon.Reset();
        Setting.Auto.Reset();
        Setting.Overlay.Reset();

        HotKeys.ClearKeys();
    }

    /// <summary>
    /// 创建导航菜单
    /// </summary>
    private void CreateNavMenus()
    {
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "自身属性", ViewName = "SelfStateView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "世界功能", ViewName = "WorldFunctionView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "线上选项", ViewName = "OnlineOptionView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "线上载具", ViewName = "SpawnVehicleView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "线上武器", ViewName = "SpawnWeaponView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "自定传送", ViewName = "CustomTeleportView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "Blip传送", ViewName = "BlipTeleportView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "随身技工", ViewName = "DriverButlerView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "任务帮手", ViewName = "JobHelperView" });
        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "玩家列表", ViewName = "PlayerListView" });

        NavMenus.Add(new NavMenu() { Icon = "\xe610", Title = "README", ViewName = "ReadMeView" });
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
            case "SelfStateView":
                ContentControl_Main.Content = SelfStateView;
                break;
            case "WorldFunctionView":
                ContentControl_Main.Content = WorldFunctionView;
                break;
            case "OnlineOptionView":
                ContentControl_Main.Content = OnlineOptionView;
                break;
            case "SpawnVehicleView":
                ContentControl_Main.Content = SpawnVehicleView;
                break;
            case "SpawnWeaponView":
                ContentControl_Main.Content = SpawnWeaponView;
                break;
            case "CustomTeleportView":
                ContentControl_Main.Content = CustomTeleportView;
                break;
            case "BlipTeleportView":
                ContentControl_Main.Content = BlipTeleportView;
                break;
            case "DriverButlerView":
                ContentControl_Main.Content = DriverButlerView;
                break;
            case "JobHelperView":
                ContentControl_Main.Content = JobHelperView;
                break;
            case "PlayerListView":
                ContentControl_Main.Content = PlayerListView;
                break;
            case "ReadMeView":
                ContentControl_Main.Content = ReadMeView;
                break;
        }
    }

    /// <summary>
    /// 外置菜单窗口是否置顶
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckBox_IsTopMost_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_IsTopMost.IsChecked == true)
            Topmost = true;
        else
            Topmost = false;
    }

    /// <summary>
    /// 全局热键 按键按下事件
    /// </summary>
    /// <param name="vK"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void HotKeys_KeyDownEvent(WinVK vK)
    {
        this.Dispatcher.Invoke(() =>
        {
            switch (vK)
            {
                case WinVK.DELETE:
                    ShowWindow();
                    break;
            }
        });
    }

    /// <summary>
    /// 显示隐藏外置菜单窗口
    /// </summary>
    private void ShowWindow()
    {
        IsShowExternalMenu = !IsShowExternalMenu;
        if (IsShowExternalMenu)
        {
            this.WindowState = WindowState.Normal;

            if (CheckBox_IsTopMost.IsChecked == false)
            {
                Topmost = true;
                Topmost = false;
            }

            Win32.SetCursorPos(ThisWinPOINT.X, ThisWinPOINT.Y);
        }
        else
        {
            this.WindowState = WindowState.Minimized;

            Win32.GetCursorPos(out ThisWinPOINT);
            Memory.SetForegroundWindow();
        }
    }

    private void CPedThread()
    {
        while (IsAppRunning)
        {
            long pCPed = Globals.GetCPed();
            long pCNavigation = Memory.Read<long>(pCPed + Offsets.CPed_CNavigation);

            byte oInVehicle = Memory.Read<byte>(pCPed + Offsets.CPed_InVehicle);

            byte oGod = Memory.Read<byte>(pCPed + Offsets.CPed_God);
            byte oRagdoll = Memory.Read<byte>(pCPed + Offsets.CPed_Ragdoll);
            byte oSeatbelt = Memory.Read<byte>(pCPed + Offsets.CPed_Seatbelt);

            ////////////////////////////////////////////////////////////////

            // 玩家无敌
            if (Setting.Player.GodMode)
            {
                if (oGod != 0x01)
                    Memory.Write<byte>(pCPed + Offsets.CPed_God, 0x01);
            }

            // 挂机防踢
            if (Setting.Player.AntiAFK)
            {
                if (Hacks.ReadGA<int>(262145 + 87) != 99999999)
                    Online.AntiAFK(true);
            }

            // 无布娃娃
            if (Setting.Player.NoRagdoll)
            {
                if (oRagdoll != 0x01)
                    Memory.Write<byte>(pCPed + Offsets.CPed_Ragdoll, 0x01);
            }

            // 玩家无碰撞体积
            if (Setting.Player.NoCollision)
            {
                long pointer = Memory.Read<long>(pCNavigation + 0x10);
                pointer = Memory.Read<long>(pointer + 0x20);
                pointer = Memory.Read<long>(pointer + 0x70);
                pointer = Memory.Read<long>(pointer + 0x00);
                Memory.Write(pointer + 0x2C, -1.0f);
            }

            // 安全带
            if (Setting.Vehicle.Seatbelt)
            {
                if (oSeatbelt != 0xC9)
                    Memory.Write<byte>(pCPed + Offsets.CPed_Seatbelt, 0xC9);
            }

            // 弹药编辑
            if (Setting.Weapon.AmmoModifierFlag != 0)
            {
                long pCPedInventory = Memory.Read<long>(pCPed + Offsets.CPed_CPedInventory);
                Memory.Write(pCPedInventory + Offsets.CPed_CPedInventory_AmmoModifier, Setting.Weapon.AmmoModifierFlag);
            }

            // 非公开战局运货
            if (Setting.Online.AllowSellOnNonPublic)
                Online.AllowSellOnNonPublic(true);

            ////////////////////////////////////////////////////////////////

            if (oInVehicle != 0x00)
            {
                long pCVehicle = Memory.Read<long>(pCPed + Offsets.CPed_CVehicle);
                byte oVehicleGod = Memory.Read<byte>(pCVehicle + Offsets.CPed_CVehicle_God);

                // 载具无敌
                if (Setting.Vehicle.GodMode)
                {
                    if (oVehicleGod != 0x01)
                        Memory.Write<byte>(pCVehicle + Offsets.CPed_CVehicle_God, 0x01);
                }
            }

            Thread.Sleep(1000);
        }
    }

    private void CommonThread()
    {
        while (IsAppRunning)
        {
            // 自动消星
            if (Setting.Auto.ClearWanted)
                Player.WantedLevel(0x00);

            long pCPedList = Globals.GetCPedList();

            for (int i = 0; i < Offsets.oMaxPeds; i++)
            {
                long pCPed = Memory.Read<long>(pCPedList + i * 0x10);
                if (!Memory.IsValid(pCPed))
                    continue;

                // 跳过玩家
                long pCPlayerInfo = Memory.Read<long>(pCPed + Offsets.CPed_CPlayerInfo);
                if (Memory.IsValid(pCPlayerInfo))
                    continue;

                // 自动击杀NPC
                if (Setting.Auto.KillNPC)
                    Memory.Write(pCPed + Offsets.CPed_Health, 0.0f);

                // 自动击杀敌对NPC
                if (Setting.Auto.KillHostilityNPC)
                {
                    byte oHostility = Memory.Read<byte>(pCPed + Offsets.CPed_Hostility);
                    if (oHostility > 0x01)
                    {
                        Memory.Write(pCPed + Offsets.CPed_Health, 0.0f);
                    }
                }

                // 自动击杀警察
                if (Setting.Auto.KillPolice)
                {
                    int ped_type = Memory.Read<int>(pCPed + Offsets.CPed_Ragdoll);
                    ped_type = ped_type << 11 >> 25;

                    if (ped_type == (int)PedType.COP ||
                        ped_type == (int)PedType.SWAT ||
                        ped_type == (int)PedType.ARMY)
                    {
                        Memory.Write(pCPed + Offsets.CPed_Health, 0.0f);
                    }
                }
            }

            Thread.Sleep(200);
        }
    }
}