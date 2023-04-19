using GTA5Menu.Data;
using GTA5Menu.Utils;
using GTA5Menu.Config;

using GTA5Core.Feature;
using GTA5Core.RAGE.Teleports;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.ExternalMenu;

/// <summary>
/// CustomTeleportView.xaml 的交互逻辑
/// </summary>
public partial class CustomTeleportView : UserControl
{
    public CustomTeleportView()
    {
        InitializeComponent();
        this.DataContext = this;
        ExternalMenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;

        // 如果配置文件存在就读取
        if (File.Exists(GTA5Util.File_Config_Teleports))
        {
            using var streamReader = new StreamReader(GTA5Util.File_Config_Teleports);
            var teleports = JsonHelper.JsonDese<Teleports>(streamReader.ReadToEnd());

            TeleportData.Custom.Clear();

            // 加载读取的配置文件到UI中，由于Json结构不一样，需要转换
            foreach (var custom in teleports.CustomLocations)
            {
                TeleportData.Custom.Add(new TeleportInfo()
                {
                    Name = custom.Name,
                    X = custom.X,
                    Y = custom.Y,
                    Z = custom.Z
                });
            }
        }

        // 加载传送分类目录
        foreach (var item in TeleportData.TeleportClasses)
        {
            ComboBox_TeleportClass.Items.Add(new IconMenu()
            {
                Icon = item.Icon,
                Title = item.Name
            });
        }
        ComboBox_TeleportClass.SelectedIndex = 0;
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {

    }

    /////////////////////////////////////////////////////////////////////////////

    #region 自定义传送
    private void ComboBox_TeleportClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var index = ComboBox_TeleportClass.SelectedIndex;
        if (index != -1)
        {
            ListBox_TeleportInfos.Items.Clear();

            foreach (var item in TeleportData.TeleportClasses[index].TeleportInfos)
            {
                ListBox_TeleportInfos.Items.Add(item);
            }

            ListBox_TeleportInfos.SelectedIndex = 0;
        }
    }

    private void Button_Teleport_Click(object sender, RoutedEventArgs e)
    {
        var index1 = ComboBox_TeleportClass.SelectedIndex;
        var index2 = ListBox_TeleportInfos.SelectedIndex;
        if (index1 != -1 && index2 != -1)
        {
            var position = TeleportData.TeleportClasses[index1].TeleportInfos[index2];
            Teleport.SetTeleportPosition(new()
            {
                X = position.X,
                Y = position.Y,
                Z = position.Z
            });
        }
    }

    private void ListBox_TeleportInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        Button_Teleport_Click(null, null);
    }

    /////////////////////////////////////////////////////////////////////////////

    private void Button_MoveDistance_Click(object sender, RoutedEventArgs e)
    {
        var moveDistance = (float)Slider_MoveDistance.Value;

        var btnContent = (e.OriginalSource as Button).Content.ToString();
        switch (btnContent)
        {
            case "向前":
                Teleport.MoveFoward(moveDistance);
                break;
            case "向后":
                Teleport.MoveBack(moveDistance);
                break;
            case "向左":
                Teleport.MoveLeft(moveDistance);
                break;
            case "向右":
                Teleport.MoveRight(moveDistance);
                break;
            case "向上":
                Teleport.MoveUp(moveDistance);
                break;
            case "向下":
                Teleport.MoveDown(moveDistance);
                break;
        }
    }
    #endregion
}
