using GTA5Menu.Data;
using GTA5Menu.Utils;
using GTA5Menu.Config;

using GTA5Core.RAGE;
using GTA5Core.Feature;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.ExternalMenu;

/// <summary>
/// BlipTeleportView.xaml 的交互逻辑
/// </summary>
public partial class BlipTeleportView : UserControl
{
    public List<BlipInfo2> MyFavorites { get; private set; } = new();

    public BlipTeleportView()
    {
        InitializeComponent();
        this.DataContext = this;
        ExternalMenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;

        // 如果配置文件存在就读取
        if (File.Exists(GTA5Util.File_Config_Blips))
        {
            using var streamReader = new StreamReader(GTA5Util.File_Config_Blips);
            var blips = JsonHelper.JsonDese<List<Blips>>(streamReader.ReadToEnd());

            // 填充数据
            foreach (var info in blips)
            {
                MyFavorites.Add(new()
                {
                    Value = info.Value,
                    Color = info.Color,
                    Image = RAGEHelper.GetBlipModelImage(info.Value),
                    Image2 = RAGEHelper.GetBlipColorImage(info.Color)
                });
            }
        }
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {

    }

    ////////////////////////////////////////////////////////////////////////////////

    private void Button_BlipTeleport_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (ListBox_BlipModels.SelectedItem is BlipInfo2 info)
        {
            Teleport.ToBlips(info.Value, info.Color);
        }
    }

    private void ListBox_BlipModels_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        Button_BlipTeleport_Click(null, null);
    }
}
