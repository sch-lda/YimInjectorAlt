using GTA5Menu.Data;

using GTA5Core.RAGE;
using GTA5Core.RAGE.Blips;
using GTA5Core.Feature;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.CustonBlip;

/// <summary>
/// AllBlipView.xaml 的交互逻辑
/// </summary>
public partial class AllBlipView : UserControl
{
    public AllBlipView()
    {
        InitializeComponent();
        this.DataContext = this;
        CustomBlipWindow.WindowClosingEvent += CustomBlipWindow_WindowClosingEvent;

        foreach (var model in BlipData.BlipModels)
        {
            ListBox_BlipModels.Items.Add(new BlipInfo()
            {
                Value = model,
                Image = RAGEHelper.GetBlipModelImage(model)
            });
        }
        ListBox_BlipModels.SelectedIndex = 0;

        foreach (var color in BlipData.BlipColors)
        {
            ListBox_BlipColors.Items.Add(new BlipInfo()
            {
                Value = color,
                Image = RAGEHelper.GetBlipColorImage(color)
            });
        }
        ListBox_BlipColors.SelectedIndex = 0;
    }

    private void CustomBlipWindow_WindowClosingEvent()
    {

    }

    private void MenuItem_TeleportToBlips_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (ListBox_BlipModels.SelectedItem is BlipInfo info)
        {
            if (ListBox_BlipColors.SelectedItem is BlipInfo info2)
                Teleport.ToBlips(info.Value, (byte)info2.Value);
            else
                Teleport.ToBlips(info.Value);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的Blip，操作取消");
        }
    }

    private void MenuItem_AddMyFavorite_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (ListBox_BlipModels.SelectedItem is BlipInfo info)
        {
            var blip2 = new BlipInfo2
            {
                Value = info.Value,
                Image = info.Image
            };

            if (ListBox_BlipColors.SelectedItem is BlipInfo info2)
            {
                blip2.Color = (byte)info2.Value;
                blip2.Image2 = info2.Image;
            }

            MyBlipView.ActionAddMyFavorite(blip2);
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
