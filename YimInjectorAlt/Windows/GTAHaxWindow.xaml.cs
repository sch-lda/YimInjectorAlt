using GTA5Menu.Utils;
using GTA5Core.RAGE.Stats;
using GTA5Shared.Helper;

namespace YimInjectorAlt.Windows;

/// <summary>
/// GTAHaxWindow.xaml 的交互逻辑
/// </summary>
public partial class GTAHaxWindow
{
    public GTAHaxWindow()
    {
        InitializeComponent();
    }

    private void Window_GTAHax_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void Window_GTAHax_Closing(object sender, CancelEventArgs e)
    {

    }

    private void TextBox_AppendText_MP(string str, string value)
    {
        TextBox_PreviewGTAHax.AppendText($"$MPx{str}\n");
        TextBox_PreviewGTAHax.AppendText($"{value}\n");
    }

    private void TextBox_AppendText_NoMP(string str, string value)
    {
        TextBox_PreviewGTAHax.AppendText($"${str}\n");
        TextBox_PreviewGTAHax.AppendText($"{value}\n");
    }

    private void ListBox_GTAHaxClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      
    }

    private void Button_ImportGTAHax_Click(object sender, RoutedEventArgs e)
    {
        var stat = TextBox_PreviewGTAHax.Text;
        if (string.IsNullOrWhiteSpace(stat))
        {
            NotifierHelper.Show(NotifierType.Warning, "stat代码不能为空，操作取消");
            return;
        }

        GTAHaxUtil.ImportGTAHax(TextBox_PreviewGTAHax.Text);
    }
}
