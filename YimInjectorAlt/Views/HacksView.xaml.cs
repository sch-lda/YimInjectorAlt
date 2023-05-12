using YimInjectorAlt.Models;
using YimInjectorAlt.Windows;
using YimInjectorAlt.Views.ReadMe;

using GTA5Inject;
using GTA5Shared.Helper;

using CommunityToolkit.Mvvm.Input;

namespace YimInjectorAlt.Views;

/// <summary>
/// HacksView.xaml 的交互逻辑
/// </summary>
public partial class HacksView : UserControl
{
    /// <summary>
    /// 数据模型绑定
    /// </summary>
    public HacksModel HacksModel { get; set; } = new();

    private readonly Kiddion Kiddion = new();
    private readonly GTAHax GTAHax = new();
    private readonly BincoHax BincoHax = new();
    private readonly LSCHax LSCHax = new();
    private readonly YimMenu YimMenu = new();

    private Kiddion2Window Kiddion2Window = null;
    private GTAHaxWindow GTAHaxWindow = null;

    public HacksView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        new Thread(CheckCheatsIsRun)
        {
            Name = "CheckCheatsIsRun",
            IsBackground = true
        }.Start();
    }

    private void MainWindow_WindowClosingEvent()
    {

    }

    /// <summary>
    /// 检查第三方辅助是否正在运行线程
    /// </summary>
    private void CheckCheatsIsRun()
    {
        while (MainWindow.IsAppRunning)
        {
            // 判断 Kiddion 是否运行
            HacksModel.KiddionIsRun = ProcessHelper.IsAppRun("Kiddion");
            // 判断 GTAHax 是否运行
            HacksModel.GTAHaxIsRun = ProcessHelper.IsAppRun("GTAHax");
            // 判断 BincoHax 是否运行
            HacksModel.BincoHaxIsRun = ProcessHelper.IsAppRun("BincoHax");
            // 判断 LSCHax 是否运行
            HacksModel.LSCHaxIsRun = ProcessHelper.IsAppRun("LSCHax");

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 点击第三方辅助开关按钮
    /// </summary>
    /// <param name="hackName"></param>
    [RelayCommand]
    private void CheatsClick(string hackName)
    {
       //

        if (ProcessHelper.IsGTA5Run())
        {
            switch (hackName)
            {
   
                case "YimMenu":
                    YimMenuClick();
                    break;
            }
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "未发现《GTA5》进程，请先运行《GTA5》游戏");
        }
    }

    /// <summary>
    /// 点击第三方辅助使用说明
    /// </summary>
    /// <param name="Name"></param>
    [RelayCommand]
    private void ReadMeClick(string Name)
    {

    }

    /// <summary>
    /// 点击第三方辅助配置文件路径
    /// </summary>
    /// <param name="funcName"></param>
    [RelayCommand]
    private void ExtraClick(string funcName)
    {
       //
    }

    /// <summary>
    /// 显示使用说明窗口
    /// </summary>
    /// <param name="userControl"></param>
    private void ShowReadMe(UserControl userControl)
    {
        var readMeWindow = new ReadMeWindow(userControl)
        {
            Owner = MainWindow.MainWindowInstance
        };
        readMeWindow.ShowDialog();
    }

    #region 第三方辅助功能开关事件


    /// <summary>
    /// YimMenu点击事件
    /// </summary>
    private void YimMenuClick()
    {
        Process GTA5Process = null;

        foreach (var item in Process.GetProcessesByName("GTA5"))
        {
            if (item.MainWindowHandle == IntPtr.Zero)
                continue;

            if (item.MainModule.FileVersionInfo.LegalCopyright.Contains("Rockstar Games Inc."))
            {
                GTA5Process = item;
                break;
            }
        }

        if (GTA5Process == null)
        {
            NotifierHelper.Show(NotifierType.Warning, "未发现正确的《GTA5》进程");
            return;
        }

        var result = Injector.DLLInjector(GTA5Process.Id, FileHelper.File_Inject_YimMenu, true);
        if (result.IsSuccess)
            NotifierHelper.Show(NotifierType.Success, "注入成功");
        else
            NotifierHelper.Show(NotifierType.Error, $"YimMenu菜单注入\n错误信息：{result.Content}");
    }
    #endregion



}
