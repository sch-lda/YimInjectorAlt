﻿using GTA5Menu.Data;
using GTA5Core.Native;
using GTA5Core.Feature;

namespace GTA5Menu.Views.ExternalMenu;

/// <summary>
/// PlayerListView.xaml 的交互逻辑
/// </summary>
public partial class PlayerListView : UserControl
{
    // 显示玩家列表
    private List<NetPlayerData> NetPlayerDatas = new();

    public PlayerListView()
    {
        InitializeComponent();
        this.DataContext = this;
        ExternalMenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {

    }

    private void Button_RefreshPlayerList_Click(object sender, RoutedEventArgs e)
    {
        lock (this)
        {
            NetPlayerDatas.Clear();
            ListBox_NetPlayer.Items.Clear();

            long pCNetworkPlayerMgr = Memory.Read<long>(Pointers.NetworkPlayerMgrPTR);

            for (int i = 0; i < 32; i++)
            {
                long pCNetGamePlayer = Memory.Read<long>(pCNetworkPlayerMgr + Offsets.CNetworkPlayerMgr_CNetGamePlayer + i * 0x08);
                if (!Memory.IsValid(pCNetGamePlayer))
                    continue;

                long pCPlayerInfo = Memory.Read<long>(pCNetGamePlayer + Offsets.CNetworkPlayerMgr_CNetGamePlayer_CPlayerInfo);
                if (!Memory.IsValid(pCPlayerInfo))
                    continue;

                long pCPed = Memory.Read<long>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_CPed);
                if (!Memory.IsValid(pCPed))
                    continue;

                ////////////////////////////////////////////

                var god = Memory.Read<byte>(pCPed + Offsets.CPed_God);
                var position = Memory.Read<Vector3>(pCPed + Offsets.CPed_VisualX);

                var money = Hacks.ReadGA<long>(1853910 + 1 + i * 862 + 205 + 56);
                var cash = Hacks.ReadGA<long>(1853910 + 1 + i * 862 + 205 + 3);

                ////////////////////////////////////////////

                NetPlayerDatas.Add(new NetPlayerData()
                {
                    Rank = Hacks.ReadGA<int>(1853910 + 1 + i * 862 + 205 + 6),

                    RockstarId = Memory.Read<long>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_RockstarID),
                    PlayerName = Memory.ReadString(pCPlayerInfo + Offsets.CPed_CPlayerInfo_Name, 20),

                    Money = money,
                    Cash = cash,
                    Bank = money - cash,

                    Health = Memory.Read<float>(pCPed + Offsets.CPed_Health),
                    MaxHealth = Memory.Read<float>(pCPed + Offsets.CPed_HealthMax),
                    Armor = Memory.Read<float>(pCPed + Offsets.CPed_Armor),
                    GodMode = (god & 1) == 1,
                    NoRagdoll = Memory.Read<byte>(pCPed + Offsets.CPed_Ragdoll) != 0x20,

                    Distance = GetDistance(Teleport.GetPlayerPosition(), position),
                    Position = position,

                    WantedLevel = Memory.Read<byte>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_WantedLevel),
                    WalkSpeed = Memory.Read<float>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_WalkSpeed),
                    RunSpeed = Memory.Read<float>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_RunSpeed),
                    SwimSpeed = Memory.Read<float>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_SwimSpeed),

                    ClanName = Memory.ReadString(pCNetGamePlayer + Offsets.CNetworkPlayerMgr_CNetGamePlayer_ClanName, 20),
                    ClanTag = Memory.ReadString(pCNetGamePlayer + Offsets.CNetworkPlayerMgr_CNetGamePlayer_ClanTag, 20),
                });
            }

            foreach (var item in NetPlayerDatas)
            {
                ListBox_NetPlayer.Items.Add(new NetPlayer()
                {
                    Rank = item.Rank,
                    Avatar = "\xe6d2",
                    Name = string.IsNullOrEmpty(item.ClanTag) ? item.PlayerName : $"{item.PlayerName} [{item.ClanTag}]",
                    RID = item.RockstarId,
                    GodMode = item.GodMode ? "无敌" : ""
                });
            }
        }
    }

    private void Button_TeleportSelectedPlayer_Click(object sender, RoutedEventArgs e)
    {
        if (ListBox_NetPlayer.SelectedItem != null)
        {
            var index = ListBox_NetPlayer.SelectedIndex;
            if (index != -1)
            {
                Teleport.SetTeleportPosition(NetPlayerDatas[index].Position);
            }
        }
    }

    private void PlayerInfoAppend(string title = "", string value = "")
    {
        ListBox_PlayerInfo.Items.Add(new IconMenu()
        {
            Icon = string.Empty,
            Title = title,
            Value = value
        });
    }

    private string BoolToString(bool value)
    {
        return value ? "ON" : "OFF";
    }

    private float GetDistance(Vector3 myPosV3, Vector3 pedPosV3)
    {
        return (float)Math.Sqrt(
            Math.Pow(myPosV3.X - pedPosV3.X, 2) +
            Math.Pow(myPosV3.Y - pedPosV3.Y, 2) +
            Math.Pow(myPosV3.Z - pedPosV3.Z, 2));
    }

    private void ListBox_NetPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ListBox_PlayerInfo.Items.Clear();

        var index = ListBox_NetPlayer.SelectedIndex;
        if (index != -1)
        {
            var item = NetPlayerDatas[index];

            PlayerInfoAppend("昵称", $"{item.PlayerName}");
            PlayerInfoAppend("RockStar ID", $"{item.RockstarId}");
            PlayerInfoAppend("帮会", $"{item.ClanTag}");
            PlayerInfoAppend("帮会名称", $"{item.ClanName}");
            PlayerInfoAppend();

            PlayerInfoAppend("等级", $"{item.Rank}");
            PlayerInfoAppend("$ 总计", $"{item.Money}");
            PlayerInfoAppend("$ 银行", $"{item.Bank}");
            PlayerInfoAppend("$ 现金", $"{item.Cash}");
            PlayerInfoAppend();

            PlayerInfoAppend("护甲值", $"{item.Armor:0.0}");
            PlayerInfoAppend("生命值", $"{item.Health:0.0}");
            PlayerInfoAppend("最大生命值", $"{item.MaxHealth:0.0}");
            PlayerInfoAppend();

            PlayerInfoAppend("无敌状态", BoolToString(item.GodMode));
            PlayerInfoAppend("无布娃娃", BoolToString(item.NoRagdoll));
            PlayerInfoAppend("通缉等级", $"{item.WantedLevel}");
            PlayerInfoAppend();

            PlayerInfoAppend("步行速度", $"{item.WalkSpeed:0.0}");
            PlayerInfoAppend("奔跑速度", $"{item.RunSpeed:0.0}");
            PlayerInfoAppend("游泳速度", $"{item.SwimSpeed:0.0}");
            PlayerInfoAppend("与我距离", $"{item.Distance:0.000}");
            PlayerInfoAppend("坐标数据", $"{item.Position.X:0.000}, {item.Position.Y:0.000}, {item.Position.Z:0.000}");
        }
    }
}
