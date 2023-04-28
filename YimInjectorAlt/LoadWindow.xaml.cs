using YimInjectorAlt.Utils;
using YimInjectorAlt.Models;

using GTA5Shared.Helper;

using CommunityToolkit.Mvvm.Input;

namespace YimInjectorAlt;

/// <summary>
/// LoadWindow.xaml 的交互逻辑
/// </summary>
public partial class LoadWindow
{
    /// <summary>
    /// 数据模型绑定
    /// </summary>
    public LoadModel LoadModel { get; set; } = new();

    public LoadWindow()
    {
        InitializeComponent();
    }

    private void Window_Load_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        Task.Run(() =>
        {
            try
            {
                // 关闭第三方进程
                ProcessHelper.CloseThirdProcess();

                LoadModel.LoadState = "正在初始化工具...";

                // LoggerHelper.Info("开始初始化程序...");
                // LoggerHelper.Info($"当前程序版本号 {CoreUtil.ClientVersion}");
                // LoggerHelper.Info($"当前程序最后编译时间 {CoreUtil.BuildDate}");

                // 客户端程序版本号
                LoadModel.VersionInfo = CoreUtil.ClientVersion;
                // 最后编译时间
                LoadModel.BuildDate = CoreUtil.BuildDate;

                /////////////////////////////////////////////////////////////////////

                LoadModel.LoadState = "正在初始化配置文件...";
                // LoggerHelper.Info("正在初始化配置文件...");

                // 创建指定文件夹，用于释放必要文件和更新软件（如果已存在则不会创建）
                Directory.CreateDirectory(FileHelper.Dir_Cache);
                Directory.CreateDirectory(FileHelper.Dir_Config);
                Directory.CreateDirectory(FileHelper.Dir_Kiddion);
                Directory.CreateDirectory(FileHelper.Dir_Kiddion_Scripts);
                Directory.CreateDirectory(FileHelper.Dir_Inject);


                // 清空缓存文件夹
                FileHelper.ClearDirectory(FileHelper.Dir_Cache);

             
                // 判断DLL文件是否存在以及是否被占用
                if (!File.Exists(FileHelper.File_Inject_YimMenu))
                {
                    FileHelper.ExtractResFile(FileHelper.Res_Inject_YimMenu, FileHelper.File_Inject_YimMenu);
                }
                else
                {
                    if (!FileHelper.IsOccupied(FileHelper.File_Inject_YimMenu))
                        FileHelper.ExtractResFile(FileHelper.Res_Inject_YimMenu, FileHelper.File_Inject_YimMenu);
                    else
                    {
                        // LoggerHelper.Error("Yimmenu未准备就绪");
                        this.Dispatcher.Invoke(() =>
                        {
                            NotifierHelper.Show(NotifierType.Error, "请卸载注入yimmenu或退出游戏，然后重启此程序直至不出现错误信息");
                            
                        });
                    }
                }

           

                /////////////////////////////////////////////////////////////////////

                this.Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    // 转移主程序控制权
                    Application.Current.MainWindow = mainWindow;
                    // 显示主窗口
                    mainWindow.Show();
                    // 关闭初始化窗口
                    this.Close();
                });
            }
            catch (Exception ex)
            {
                LoadModel.LoadState = $"初始化错误，发生了未知异常！\n{ex.Message}";
                // LoggerHelper.Error("初始化错误，发生了未知异常", ex);

                this.Dispatcher.Invoke(() =>
                {
                    WrapPanel_ExceptionState.Visibility = Visibility.Visible;
                });
            }
        });
    }

    [RelayCommand]
    private void ButtonClick(string name)
    {
        switch (name)
        {
            case "OpenDefaultPath":
                ProcessHelper.OpenProcess(FileHelper.Dir_Default);
                break;
            case "ExitAPP":
                Application.Current.Shutdown();
                break;
        }
    }
}
