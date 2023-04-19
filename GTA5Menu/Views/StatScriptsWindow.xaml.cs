using GTA5Core.Feature;
using GTA5Core.RAGE.Stats;

namespace GTA5Menu.Views;

/// <summary>
/// StatScriptsWindow.xaml 的交互逻辑
/// </summary>
public partial class StatScriptsWindow
{
    public StatScriptsWindow()
    {
        InitializeComponent();
    }

    private void Window_StatScripts_Loaded(object sender, RoutedEventArgs e)
    {
        // STAT列表

    }

    private void Window_StatScripts_Closing(object sender, CancelEventArgs e)
    {

    }

    private void Button_LoadSession_Click(object sender, RoutedEventArgs e)
    {
        Online.LoadSession(11);
    }

    private void AppendTextBox(string log)
    {
        this.Dispatcher.Invoke(() =>
        {
            TextBox_Logger.AppendText($"[{DateTime.Now:T}] {log}\n");
            TextBox_Logger.ScrollToEnd();
        });
    }

    private void Button_ExecuteAutoScript_Click(object sender, RoutedEventArgs e)
    {
        var index = ListBox_STATList.SelectedIndex;
        if (index != -1)
        {
            AutoScript(ListBox_STATList.SelectedItem.ToString());
        }
    }

    private void AutoScript(string statClassName)
    {
        TextBox_Logger.Clear();

        Task.Run(async () =>
        {
            try
            {
               
                
            }
            catch (Exception ex)
            {
                AppendTextBox($"错误：{ex.Message}");
            }
        });
    }
}
