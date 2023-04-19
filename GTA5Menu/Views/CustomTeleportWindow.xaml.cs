using GTA5Menu.Utils;
using GTA5Menu.Config;

using GTA5Core.Feature;
using GTA5Core.RAGE.Teleports;
using GTA5Shared.Helper;

namespace GTA5Menu.Views;

/// <summary>
/// CustomTeleportWindow.xaml 的交互逻辑
/// </summary>
public partial class CustomTeleportWindow
{
    public CustomTeleportWindow()
    {
        InitializeComponent();
    }

    private void Window_CustomTeleport_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        // 如果配置文件不存在就创建
        if (!File.Exists(GTA5Util.File_Config_Teleports))
        {
            // 保存配置文件
            SaveConfig();
        }

        // 如果配置文件存在就读取
        if (File.Exists(GTA5Util.File_Config_Teleports))
        {
            using var streamReader = new StreamReader(GTA5Util.File_Config_Teleports);
            var teleports = JsonHelper.JsonDese<Teleports>(streamReader.ReadToEnd());

            // 加载读取的配置文件到UI中，由于Json结构不一样，需要转换
            foreach (var custom in teleports.CustomLocations)
            {
                ListBox_Teleports.Items.Add(new TeleportInfo()
                {
                    Name = custom.Name,
                    X = custom.X,
                    Y = custom.Y,
                    Z = custom.Z
                });
            }
        }
    }

    private void Window_CustomTeleport_Closing(object sender, CancelEventArgs e)
    {
        SaveConfig();
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    private void SaveConfig()
    {
        if (Directory.Exists(FileHelper.Dir_Config))
        {
            var teleports = new Teleports
            {
                CustomLocations = new()
            };
            // 加载到配置文件
            foreach (TeleportInfo info in ListBox_Teleports.Items)
            {
                teleports.CustomLocations.Add(new()
                {
                    Name = info.Name,
                    X = info.X,
                    Y = info.Y,
                    Z = info.Z,
                    Pitch = 0.0f,
                    Yaw = 0.0f,
                    Roll = 0.0f,
                });
            }
            // 写入到Json文件
            File.WriteAllText(GTA5Util.File_Config_Teleports, JsonHelper.JsonSeri(teleports));
        }
    }

    private void Button_AddCustomTeleport_Click(object sender, RoutedEventArgs e)
    {
        var vector3 = Teleport.GetPlayerPosition();

        ListBox_Teleports.Items.Add(new TeleportInfo()
        {
            Name = $"保存点 : {DateTime.Now:yyyyMMdd_HHmmss_ffff}",
            X = vector3.X,
            Y = vector3.Y,
            Z = vector3.Z
        });

        ListBox_Teleports.SelectedIndex = ListBox_Teleports.Items.Count - 1;
    }

    private void Button_EditCustomTeleport_Click(object sender, RoutedEventArgs e)
    {
        var tempName = TextBox_CustomName.Text.Trim();
        var tempX = TextBox_Position_X.Text.Trim();
        var tempY = TextBox_Position_Y.Text.Trim();
        var tempZ = TextBox_Position_Z.Text.Trim();

        if (string.IsNullOrEmpty(tempName))
        {
            NotifierHelper.Show(NotifierType.Warning, "坐标名称不能为空，操作取消");
            return;
        }

        if (!float.TryParse(tempX, out float x) ||
            !float.TryParse(tempY, out float y) ||
            !float.TryParse(tempZ, out float z))
        {
            NotifierHelper.Show(NotifierType.Warning, "坐标数据不合法，操作取消");
            return;
        }

        if (ListBox_Teleports.SelectedItem is TeleportInfo item)
        {
            item.Name = tempName;
            item.X = x;
            item.Y = y;
            item.Z = z;

            ListBox_Teleports.SelectedItem = item;
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "当前自定义传送坐标选中项为空");
        }
    }

    private void Button_DeleteCustomTeleport_Click(object sender, RoutedEventArgs e)
    {
        if (ListBox_Teleports.SelectedItem is TeleportInfo item)
        {
            ListBox_Teleports.Items.Remove(item);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "当前自定义传送坐标选中项为空");
        }
    }

    private void ListBox_Teleports_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        Button_Teleport_Click(null, null);
    }

    private void Button_Teleport_Click(object sender, RoutedEventArgs e)
    {
        if (ListBox_Teleports.SelectedItem is TeleportInfo item)
        {
            Teleport.SetTeleportPosition(new()
            {
                X = item.X,
                Y = item.Y,
                Z = item.Z
            });
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "当前自定义传送坐标选中项为空");
        }
    }

    /////////////////////////////////////////////////////////////////////////////

    private void Button_ToWaypoint_Click(object sender, RoutedEventArgs e)
    {
        Teleport.ToWaypoint();
    }

    private void Button_ToObjective_Click(object sender, RoutedEventArgs e)
    {
        Teleport.ToObjective();
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
}
