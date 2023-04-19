﻿using GTA5Core.Feature;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.HeistsEdit;

/// <summary>
/// ApartmentView.xaml 的交互逻辑
/// </summary>
public partial class ApartmentView : UserControl
{
    public ApartmentView()
    {
        InitializeComponent();
    }

    #region 公寓抢劫 - 分红数据
    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        // 公寓抢劫玩家分红比例
        TextBox_Apart_Player1.Text = Hacks.ReadGA<int>(1937658 + 3008 + 1).ToString();
        TextBox_Apart_Player2.Text = Hacks.ReadGA<int>(1937658 + 3008 + 2).ToString();
        TextBox_Apart_Player3.Text = Hacks.ReadGA<int>(1937658 + 3008 + 3).ToString();
        TextBox_Apart_Player4.Text = Hacks.ReadGA<int>(1937658 + 3008 + 4).ToString();

        TextBox_Apart_Fleeca.Text = Hacks.ReadGA<int>(262145 + 9128 + 0).ToString();
        TextBox_Apart_PrisonBreak.Text = Hacks.ReadGA<int>(262145 + 9128 + 1).ToString();
        TextBox_Apart_HumaneLabs.Text = Hacks.ReadGA<int>(262145 + 9128 + 2).ToString();
        TextBox_Apart_SeriesA.Text = Hacks.ReadGA<int>(262145 + 9128 + 3).ToString();
        TextBox_Apart_PacificStandard.Text = Hacks.ReadGA<int>(262145 + 9128 + 4).ToString();
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (TextBox_Apart_Player1.Text.Trim() != "" &&
                TextBox_Apart_Player2.Text.Trim() != "" &&
                TextBox_Apart_Player3.Text.Trim() != "" &&
                TextBox_Apart_Player4.Text.Trim() != "" &&

                TextBox_Apart_Fleeca.Text.Trim() != "" &&
                TextBox_Apart_PrisonBreak.Text.Trim() != "" &&
                TextBox_Apart_HumaneLabs.Text.Trim() != "" &&
                TextBox_Apart_SeriesA.Text.Trim() != "" &&
                TextBox_Apart_PacificStandard.Text.Trim() != "")
            {
                // 公寓抢劫玩家分红比例
                Hacks.WriteGA(1937658 + 3008 + 1, Convert.ToInt32(TextBox_Apart_Player1.Text.Trim()));
                Hacks.WriteGA(1937658 + 3008 + 2, Convert.ToInt32(TextBox_Apart_Player2.Text.Trim()));
                Hacks.WriteGA(1937658 + 3008 + 3, Convert.ToInt32(TextBox_Apart_Player3.Text.Trim()));
                Hacks.WriteGA(1937658 + 3008 + 4, Convert.ToInt32(TextBox_Apart_Player4.Text.Trim()));

                Hacks.WriteGA(262145 + 9128 + 0, Convert.ToInt32(TextBox_Apart_Fleeca.Text.Trim()));
                Hacks.WriteGA(262145 + 9128 + 1, Convert.ToInt32(TextBox_Apart_PrisonBreak.Text.Trim()));
                Hacks.WriteGA(262145 + 9128 + 2, Convert.ToInt32(TextBox_Apart_HumaneLabs.Text.Trim()));
                Hacks.WriteGA(262145 + 9128 + 3, Convert.ToInt32(TextBox_Apart_SeriesA.Text.Trim()));
                Hacks.WriteGA(262145 + 9128 + 4, Convert.ToInt32(TextBox_Apart_PacificStandard.Text.Trim()));
            }
            else
            {
                NotifierHelper.Show(NotifierType.Warning, "部分数据为空，请检查后重新写入");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
    #endregion

    #region 公寓抢劫 - 高级
    private async void WriteStatWithDelay(string hash, int value)
    {
        await Hacks.WriteIntStat(hash, value);
    }

    ////////////////////////////////////////////////////

    private void Button_HEIST_PLANNING_STAGE_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_HEIST_PLANNING_STAGE", -1);
    }
    #endregion
}
