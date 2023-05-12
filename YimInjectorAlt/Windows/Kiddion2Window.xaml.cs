using YimInjectorAlt.Utils;

using GTA5Shared.API;
using GTA5Shared.Helper;
using GTA5Core.Native;

using CommunityToolkit.Mvvm.Input;

namespace YimInjectorAlt.Windows;

/// <summary> 
/// Kiddion2Window.xaml 的交互逻辑
/// </summary>
public partial class Kiddion2Window
{
    public Kiddion2Window()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    private void Window_Kiddion2_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void Window_Kiddion2_Closing(object sender, CancelEventArgs e)
    {

    }

    /// <summary>
    /// 超链接请求导航事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessHelper.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }

    private List<string> GetKiddionUIList()
    {
        var textLists = new List<string>();

        var pArray = Process.GetProcessesByName("Kiddion");
        if (pArray.Length > 0)
        {
            var main_handle = pArray[0].MainWindowHandle;

            var button_handle = Win32.FindWindowEx(main_handle, IntPtr.Zero, "Button", null);
            while (button_handle != IntPtr.Zero)
            {
                var length = Win32.GetWindowTextLength(button_handle);
                var build = new StringBuilder(length + 1);
                _ = Win32.GetWindowText(button_handle, build, build.Capacity);

                var text = build.ToString();
                var split = text.IndexOf("|");
                if (split != -1)
                    text = text[..split];

                button_handle = Win32.FindWindowEx(main_handle, button_handle, "Button", null);
                textLists.Add($"{text}");
            }
        }

        return textLists;
    }

    private void AppendText(string str)
    {
        TextBox_UIText.AppendText($"{str}\n");
    }

    /// <summary>
    /// 获取Kiddion文本
    /// </summary>
    [RelayCommand]
    private async Task GetKiddionUI()
    {
       //

        if (!ProcessHelper.IsAppRun("Kiddion"))
        {
            NotifierHelper.Show(NotifierType.Warning, "未发现《Kiddion》进程，请先运行《Kiddion》程序");
            return;
        }

        TextBox_UIText.Clear();
        foreach (var text in GetKiddionUIList())
        {
            AppendText($"{{ L\"{text}\", L\"中文翻译\" }},");
            await Task.Delay(1);
        }
    }

    /// <summary>
    /// 批量翻译
    /// </summary>
    [RelayCommand]
    private async Task BatchTranslation()
    {
       //

        if (!ProcessHelper.IsAppRun("Kiddion"))
        {
            NotifierHelper.Show(NotifierType.Warning, "未发现《Kiddion》进程，请先运行《Kiddion》程序");
            return;
        }

        TextBox_UIText.Clear();
        foreach (var text in GetKiddionUIList())
        {
            var chs = await WebAPI.GetYouDaoContent(text);
            AppendText($"{{ L\"{text}\", L\"{chs}\" }},");
        }
    }
}
