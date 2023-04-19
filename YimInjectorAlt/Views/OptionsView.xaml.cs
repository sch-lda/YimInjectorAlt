using YimInjectorAlt.Utils;

using GTA5Shared.Helper;

namespace YimInjectorAlt.Views;

/// <summary>
/// OptionsView.xaml 的交互逻辑
/// </summary>
public partial class OptionsView : UserControl
{
    public OptionsView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        var clickAudio = IniHelper.ReadValue("Options", "ClickAudio");
        switch (clickAudio)
        {
           
            ////////////////////////
            case "0":
            default:
                AudioHelper.ClickAudio = AudioHelper.Audio.None;
                RadioButton_ClickAudio0.IsChecked = true;
                break;
        }

        TextBlock_Computer.Text = $"{Environment.MachineName}";
        TextBlock_Runtime.Text = $"{RuntimeInformation.FrameworkDescription}";
        TextBlock_Version.Text = $"{CoreUtil.ClientVersion}";
        TextBlock_Build.Text = $"{CoreUtil.BuildDate}";
    }

    /// <summary>
    /// 主窗口关闭事件
    /// </summary>
    private void MainWindow_WindowClosingEvent()
    {
        SaveConfig();
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    private void SaveConfig()
    {
        IniHelper.WriteValue("Options", "ClickAudio", $"{(int)AudioHelper.ClickAudio}");
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

    private void RadioButton_ClickAudio_Click(object sender, RoutedEventArgs e)
    {
       
 
    }
}
