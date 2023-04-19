using YimInjectorAlt.Utils;
using YimInjectorAlt.Windows;

using GTA5Core.Views;
using GTA5Shared.Helper;

using CommunityToolkit.Mvvm.Input;

namespace YimInjectorAlt.Views;

/// <summary>
/// ToolsView.xaml 的交互逻辑
/// </summary>
public partial class ToolsView : UserControl
{
    public ToolsView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;
    }

    private void MainWindow_WindowClosingEvent()
    {

    }

    /// <summary>
    /// 工具按钮点击
    /// </summary>
    /// <param name="name"></param>
    [RelayCommand]
    private void ToolsButtonClick(string name)
    {
        AudioHelper.PlayClickSound();

        switch (name)
        {
            #region 分组1
            case "CurrentDirectory":
                CurrentDirectoryClick();
                break;
            case "ReleaseDirectory":
                ReleaseDirectoryClick();
                break;
            case "InitCPDPath":
                InitCPDPathClick();
                break;
            case "RestartApp":
                RestartAppClick();
                break;
            case "OpenUpdateWindow":
                OpenUpdateWindowClick();
                break;
            case "ReNameAppCN":
                ReNameAppCNClick();
                break;
            case "ReNameAppEN":
                ReNameAppENClick();
                break;
            case "StoryModeArchive":
                StoryModeArchiveClick();
                break;
            case "ReInitGTA5Mem":
                ReInitGTA5MemClick();
                break;
            case "ManualGC":
                ManualGCClick();
                break;
            #endregion
            ////////////////////////////////////
            #region 分组2
            case "RefreshDNSCache":
                RefreshDNSCacheClick();
                break;
            case "EditHosts":
                EditHostsClick();
                break;
            case "OpenWFMSC":
                OpenWFMSCClick();
                break;
                #endregion
        }
    }

    ////////////////////////////////////////////////////////////////////////

    #region 分组1
    /// <summary>
    /// 程序当前目录
    /// </summary>
    private void CurrentDirectoryClick()
    {
        ProcessHelper.OpenLink(FileUtil.Dir_MainApp);
    }

    /// <summary>
    /// 程序释放目录
    /// </summary>
    private void ReleaseDirectoryClick()
    {
        ProcessHelper.OpenLink(FileHelper.Dir_Default);
    }

    /// <summary>
    /// 初始化配置文件夹
    /// </summary>
    private async void InitCPDPathClick()
    {
        try
        {
            if (MessageBox.Show("你确定要初始化配置文件吗？将恢复小助手全部配置文件为默认版本，对于修复崩溃问题很有帮助\n\n" +
                $"程序会自动重置此文件夹：\n{FileHelper.Dir_Default}",
                "初始化配置文件", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ProcessHelper.CloseThirdProcess();
                await Task.Delay(100);
                FileHelper.ClearDirectory(FileHelper.Dir_Default);
                await Task.Delay(100);

                App.AppMainMutex.Dispose();
                ProcessHelper.OpenProcess(FileUtil.File_MainApp);
                Application.Current.Shutdown();
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 重启程序
    /// </summary>
    private void RestartAppClick()
    {
        ProcessHelper.CloseThirdProcess();
        App.AppMainMutex.Dispose();
        ProcessHelper.OpenProcess(FileUtil.File_MainApp);
        Application.Current.Shutdown();
    }

    /// <summary>
    /// 打开更新窗口
    /// </summary>
    private void OpenUpdateWindowClick()
    {
        var UpdateWindow = new UpdateWindow
        {
            Owner = MainWindow.MainWindowInstance
        };
        UpdateWindow.ShowDialog();
    }

    /// <summary>
    /// 重命名小助手为中文
    /// </summary>
    private async void ReNameAppCNClick()
    {
        try
        {
            string fileName = Path.GetFileName(FileUtil.File_MainApp);
            var name = $"{CoreUtil.MainAppWindowName}{CoreUtil.ClientVersion}.exe";
            if (fileName != name)
            {
                var fullPath = FileUtil.GetCurrFullPath(name);
                FileUtil.FileReName(FileUtil.File_MainApp, fullPath);
                await Task.Delay(100);

                ProcessHelper.CloseThirdProcess();
                App.AppMainMutex.Dispose();
                ProcessHelper.OpenProcess(fullPath);
                Application.Current.Shutdown();
            }
            else
            {
                NotifierHelper.Show(NotifierType.Notification, "程序文件名已经符合中文命名标准，无需继续重命名");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 重命名小助手为英文
    /// </summary>
    private async void ReNameAppENClick()
    {
        try
        {
            string fileName = Path.GetFileName(FileUtil.File_MainApp);
            if (fileName != "YimInjectorAlt.exe")
            {
                FileUtil.FileReName(FileUtil.File_MainApp, "YimInjectorAlt.exe");
                await Task.Delay(100);

                ProcessHelper.CloseThirdProcess();
                App.AppMainMutex.Dispose();
                ProcessHelper.OpenProcess(FileUtil.GetCurrFullPath("YimInjectorAlt.exe"));
                Application.Current.Shutdown();
            }
            else
            {
                NotifierHelper.Show(NotifierType.Notification, "程序文件名已经符合英文命名标准，无需继续重命名");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 故事模式完美存档
    /// </summary>
    private void StoryModeArchiveClick()
    {
        var path = Path.Combine(FileHelper.Dir_MyDocuments, "Rockstar Games\\GTA V\\Profiles");
        if (!Directory.Exists(path))
        {
            NotifierHelper.Show(NotifierType.Error, "GTA5故事模式存档路径不存在，操作取消");
            return;
        }

        if (MessageBox.Show("你确定替换GTA5故事模式存档吗？将替换GTA5正版故事模式默认存档（存档进度：100%）",
            "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        {
            try
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    var dirIf = new DirectoryInfo(dir);
                    string fullName = Path.Combine(dirIf.FullName, "SGTA50000");
                    FileHelper.ExtractResFile(FileHelper.Res_Other_SGTA50000, fullName);
                }

                NotifierHelper.Show(NotifierType.Success, $"GTA5故事模式存档替换成功\n{path}");
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowException(ex);
            }
        }
    }

    /// <summary>
    /// 重新初始化GTA5内存模块
    /// </summary>
    private void ReInitGTA5MemClick()
    {
        GTA5View.ActionCloseAllGTA5Window();

        // GTA5内存模块初始化窗口
        var gTA5InitWindow = new GTA5InitWindow(false)
        {
            Owner = MainWindow.MainWindowInstance
        };
        gTA5InitWindow.ShowDialog();
    }

    /// <summary>
    /// GC垃圾回收
    /// </summary>
    private void ManualGCClick()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        NotifierHelper.Show(NotifierType.Notification, "执行GC垃圾回收成功");
    }
    #endregion

    ////////////////////////////////////////////////////////////////////////

    #region 分组2
    /// <summary>
    /// 刷新DNS缓存
    /// </summary>
    private void RefreshDNSCacheClick()
    {
        HttpHelper.FlushDNSCache();
        NotifierHelper.Show(NotifierType.Success, "成功刷新DNS解析程序缓存");
    }

    /// <summary>
    /// 编辑Hosts文件
    /// </summary>
    private void EditHostsClick()
    {
        ProcessHelper.Notepad2EditTextFile(@"C:\windows\system32\drivers\etc\hosts");
    }

    /// <summary>
    /// 打开防火墙高级设置
    /// </summary>
    private void OpenWFMSCClick()
    {
        ProcessHelper.OpenLink("wf.msc");
    }
    #endregion
}
