﻿using GTA5Core.Feature;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.HeistsEdit;

/// <summary>
/// DoomsdayView.xaml 的交互逻辑
/// </summary>
public partial class DoomsdayView : UserControl
{
    public DoomsdayView()
    {
        InitializeComponent();
    }

    #region 末日抢劫 - 分红数据
    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        // 末日抢劫玩家分红比例
        TextBox_Doomsday_Player1.Text = Hacks.ReadGA<int>(1966831 + 812 + 50 + 1).ToString();
        TextBox_Doomsday_Player2.Text = Hacks.ReadGA<int>(1966831 + 812 + 50 + 2).ToString();
        TextBox_Doomsday_Player3.Text = Hacks.ReadGA<int>(1966831 + 812 + 50 + 3).ToString();
        TextBox_Doomsday_Player4.Text = Hacks.ReadGA<int>(1966831 + 812 + 50 + 4).ToString();

        TextBox_Doomsday_ActI.Text = Hacks.ReadGA<int>(262145 + 9133 + 0).ToString();
        TextBox_Doomsday_ActII.Text = Hacks.ReadGA<int>(262145 + 9133 + 1).ToString();
        TextBox_Doomsday_ActIII.Text = Hacks.ReadGA<int>(262145 + 9133 + 2).ToString();
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (TextBox_Doomsday_Player1.Text.Trim() != "" &&
                TextBox_Doomsday_Player2.Text.Trim() != "" &&
                TextBox_Doomsday_Player3.Text.Trim() != "" &&
                TextBox_Doomsday_Player4.Text.Trim() != "" &&

                TextBox_Doomsday_ActI.Text.Trim() != "" &&
                TextBox_Doomsday_ActII.Text.Trim() != "" &&
                TextBox_Doomsday_ActIII.Text.Trim() != "")
            {
                // 末日抢劫玩家分红比例
                Hacks.WriteGA(1966831 + 812 + 50 + 1, Convert.ToInt32(TextBox_Doomsday_Player1.Text.Trim()));
                Hacks.WriteGA(1966831 + 812 + 50 + 2, Convert.ToInt32(TextBox_Doomsday_Player2.Text.Trim()));
                Hacks.WriteGA(1966831 + 812 + 50 + 3, Convert.ToInt32(TextBox_Doomsday_Player3.Text.Trim()));
                Hacks.WriteGA(1966831 + 812 + 50 + 4, Convert.ToInt32(TextBox_Doomsday_Player4.Text.Trim()));

                Hacks.WriteGA(262145 + 9133 + 0, Convert.ToInt32(TextBox_Doomsday_ActI.Text.Trim()));
                Hacks.WriteGA(262145 + 9133 + 1, Convert.ToInt32(TextBox_Doomsday_ActII.Text.Trim()));
                Hacks.WriteGA(262145 + 9133 + 2, Convert.ToInt32(TextBox_Doomsday_ActIII.Text.Trim()));
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

    #region 末日抢劫 - 高级
    private async void WriteStatWithDelay(string hash, int value)
    {
        await Hacks.WriteIntStat(hash, value);
    }

    ////////////////////////////////////////////////////
    private void Button_GANGOPS_FLOW_MISSION_PROG_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", -1);
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_503_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", 503);
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 819193);
        WriteStatWithDelay("_GANGOPS_FLOW_NOTIFICATIONS", 1557);
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_240_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", 240);
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 819198);
        WriteStatWithDelay("_GANGOPS_FLOW_NOTIFICATIONS", 1557);
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_16368_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", 16368);
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 819190);
        WriteStatWithDelay("_GANGOPS_FLOW_NOTIFICATIONS", 1557);
    }
    #endregion
}
