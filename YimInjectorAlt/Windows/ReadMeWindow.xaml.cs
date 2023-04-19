namespace YimInjectorAlt.Windows;

/// <summary>
/// ReadMeWindow.xaml 的交互逻辑
/// </summary>
public partial class ReadMeWindow
{
    public ReadMeWindow(UserControl userControl)
    {
        InitializeComponent();

        ContentControl_Main.Content = userControl;
    }
}
