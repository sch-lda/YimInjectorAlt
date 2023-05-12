using GTA5Menu.Data;
using GTA5Menu.Utils;
using GTA5Menu.Config;

using GTA5Core.RAGE;
using GTA5Core.Feature;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.CustonBlip;

/// <summary>
/// MyBlipView.xaml 的交互逻辑
/// </summary>
public partial class MyBlipView : UserControl
{
    public ObservableCollection<BlipInfo2> MyFavorites { get; private set; } = new();

    public static Action<BlipInfo2> ActionAddMyFavorite;

    public MyBlipView()
    {
        InitializeComponent();
        this.DataContext = this;
        CustomBlipWindow.WindowClosingEvent += CustomBlipWindow_WindowClosingEvent;

        ActionAddMyFavorite = AddMyFavorite;

        // 如果配置文件不存在就创建
        if (!File.Exists(GTA5Util.File_Config_Blips))
        {
            // 保存配置文件
            SaveConfig();
        }

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

    private void CustomBlipWindow_WindowClosingEvent()
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
            var blips = new List<Blips>();
            foreach (BlipInfo2 info in ListBox_BlipModels.Items)
            {
                blips.Add(new()
                {
                    Value = info.Value,
                    Color = info.Color,
                });
            }
            // 写入到Json文件
            File.WriteAllText(GTA5Util.File_Config_Blips, JsonHelper.JsonSeri(blips));
        }
    }

    private void AddMyFavorite(BlipInfo2 info)
    {
        if (!MyFavorites.Any(f => f.Value == info.Value && f.Color == info.Color))
        {
            MyFavorites.Add(info);

            NotifierHelper.Show(NotifierType.Success, $"添加Blip {info.Value} 到我的收藏成功");
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, $"Blip {info.Value} 已存在，请勿重复添加");
        }
    }

    private void MenuItem_TeleportToBlips_Click(object sender, RoutedEventArgs e)
    {
       //

        if (ListBox_BlipModels.SelectedItem is BlipInfo2 info)
        {
            Teleport.ToBlips(info.Value, info.Color);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的Blip，操作取消");
        }
    }

    private void MenuItem_DeleteMyFavorite_Click(object sender, RoutedEventArgs e)
    {
       //

        if (ListBox_BlipModels.SelectedItem is BlipInfo2 info)
        {
            MyFavorites.Remove(info);

            NotifierHelper.Show(NotifierType.Success, $"从我的收藏删除Blip {info.Value} 成功");
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的Blip，操作取消");
        }
    }

    private void ListBox_BlipModels_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        MenuItem_TeleportToBlips_Click(null, null);
    }
}
