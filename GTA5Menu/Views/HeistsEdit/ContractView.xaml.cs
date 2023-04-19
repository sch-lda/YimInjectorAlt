﻿using GTA5Core.Feature;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.HeistsEdit;

/// <summary>
/// ContractView.xaml 的交互逻辑
/// </summary>
public partial class ContractView : UserControl
{
    public ContractView()
    {
        InitializeComponent();
    }

    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        // hash: -920277662
        TextBox_FIXER_Value.Text = Hacks.ReadGA<int>(262145 + 31747).ToString();            // -2108119120 joaat("FIXER_FINALE_LEADER_CASH_REWARD")
        TextBox_TUNER_Value0.Text = Hacks.ReadGA<int>(262145 + 31042 + 1).ToString();       // -920277662
        TextBox_TUNER_Value1.Text = Hacks.ReadGA<int>(262145 + 31042 + 2).ToString();
        TextBox_TUNER_Value2.Text = Hacks.ReadGA<int>(262145 + 31042 + 3).ToString();
        TextBox_TUNER_Value3.Text = Hacks.ReadGA<int>(262145 + 31042 + 4).ToString();
        TextBox_TUNER_Value4.Text = Hacks.ReadGA<int>(262145 + 31042 + 5).ToString();
        TextBox_TUNER_Value5.Text = Hacks.ReadGA<int>(262145 + 31042 + 6).ToString();
        TextBox_TUNER_Value6.Text = Hacks.ReadGA<int>(262145 + 31042 + 7).ToString();
        TextBox_TUNER_Value7.Text = Hacks.ReadGA<int>(262145 + 31042 + 8).ToString();
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (TextBox_FIXER_Value.Text.Trim() != "" &&
                TextBox_TUNER_Value0.Text.Trim() != "" &&
                TextBox_TUNER_Value1.Text.Trim() != "" &&
                TextBox_TUNER_Value2.Text.Trim() != "" &&
                TextBox_TUNER_Value3.Text.Trim() != "" &&
                TextBox_TUNER_Value4.Text.Trim() != "" &&
                TextBox_TUNER_Value5.Text.Trim() != "" &&
                TextBox_TUNER_Value6.Text.Trim() != "" &&
                TextBox_TUNER_Value7.Text.Trim() != "")
            {
                Hacks.WriteGA(262145 + 31747, Convert.ToInt32(TextBox_FIXER_Value.Text.Trim()));
                Hacks.WriteGA(262145 + 31042 + 1, Convert.ToInt32(TextBox_TUNER_Value0.Text.Trim()));
                Hacks.WriteGA(262145 + 31042 + 2, Convert.ToInt32(TextBox_TUNER_Value1.Text.Trim()));
                Hacks.WriteGA(262145 + 31042 + 3, Convert.ToInt32(TextBox_TUNER_Value2.Text.Trim()));
                Hacks.WriteGA(262145 + 31042 + 4, Convert.ToInt32(TextBox_TUNER_Value3.Text.Trim()));
                Hacks.WriteGA(262145 + 31042 + 5, Convert.ToInt32(TextBox_TUNER_Value4.Text.Trim()));
                Hacks.WriteGA(262145 + 31042 + 6, Convert.ToInt32(TextBox_TUNER_Value5.Text.Trim()));
                Hacks.WriteGA(262145 + 31042 + 7, Convert.ToInt32(TextBox_TUNER_Value6.Text.Trim()));
                Hacks.WriteGA(262145 + 31042 + 8, Convert.ToInt32(TextBox_TUNER_Value7.Text.Trim()));
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

    private async void WriteStatWithDelay(string hash, int value)
    {
        await Hacks.WriteIntStat(hash, value);
    }

    private void Button_FIXER_GENERAL_BS_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_FIXER_GENERAL_BS", -1);
        WriteStatWithDelay("_FIXER_STORY_BS", 4095);
    }

    private void Button_TUNER_CURRENT_Click(object sender, RoutedEventArgs e)
    {
        var index = ComboBox_TUNER_CURRENT.SelectedIndex;
        if (index != -1)
        {
            WriteStatWithDelay("_TUNER_CURRENT", index);
            WriteStatWithDelay("_TUNER_GEN_BS", 65535);
        }
    }
}
