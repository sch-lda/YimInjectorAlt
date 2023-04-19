using GTA5Menu.Models;

using GTA5Core.Native;
using GTA5Core.Feature;
using GTA5Shared.Helper;

using GTA5HotKey;

namespace GTA5Menu.Views.ExternalMenu;

/// <summary>
/// SelfStateView.xaml 的交互逻辑
/// </summary>
public partial class SelfStateView : UserControl
{
    /// <summary>
    /// SelfState 的数据模型绑定
    /// </summary>
    public SelfStateModel SelfStateModel { get; set; } = new();

    /////////////////////////////////////////////////////////

    /// <summary>
    /// 判断程序是否在运行，用于结束线程
    /// </summary>
    private bool IsAppRunning = true;

    /// <summary>
    /// 穿墙开关切换
    /// </summary>
    private bool Toggle_NoCollision = false;

    /// <summary>
    /// 坐标微调距离
    /// </summary>
    private float MoveDistance = 1.5f;

    /////////////////////////////////////////////////////////

    public SelfStateView()
    {
        InitializeComponent();
        this.DataContext = this;
        ExternalMenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;

        new Thread(SelfStateUpdateThread)
        {
            Name = "SelfStateUpdateThread",
            IsBackground = true
        }.Start();

        HotKeys.AddKey(WinVK.F3);
        HotKeys.AddKey(WinVK.F4);
        HotKeys.AddKey(WinVK.F5);
        HotKeys.AddKey(WinVK.F6);
        HotKeys.AddKey(WinVK.F7);
        HotKeys.AddKey(WinVK.F8);
        HotKeys.AddKey(WinVK.Oem0);
        HotKeys.KeyDownEvent += HotKeys_KeyDownEvent;

        ///////////  读取配置文件  ///////////

        var isHotKeyToWaypoint = IniHelper.ReadValue("ExternalMenu", "IsHotKeyToWaypoint");
        var isHotKeyToObjective = IniHelper.ReadValue("ExternalMenu", "IsHotKeyToObjective");
        var isHotKeyFillHealthArmor = IniHelper.ReadValue("ExternalMenu", "IsHotKeyFillHealthArmor");
        var isHotKeyClearWanted = IniHelper.ReadValue("ExternalMenu", "IsHotKeyClearWanted");

        var isHotKeyFillAllAmmo = IniHelper.ReadValue("ExternalMenu", "IsHotKeyFillAllAmmo");
        var isHotKeyMovingFoward = IniHelper.ReadValue("ExternalMenu", "IsHotKeyMovingFoward");

        var isHotKeyNoCollision = IniHelper.ReadValue("ExternalMenu", "IsHotKeyNoCollision");

        if (!string.IsNullOrEmpty(isHotKeyToWaypoint))
            SelfStateModel.IsHotKeyToWaypoint = isHotKeyToWaypoint == "1";
        if (!string.IsNullOrEmpty(isHotKeyToObjective))
            SelfStateModel.IsHotKeyToObjective = isHotKeyToObjective == "1";
        if (!string.IsNullOrEmpty(isHotKeyFillHealthArmor))
            SelfStateModel.IsHotKeyFillHealthArmor = isHotKeyFillHealthArmor == "1";
        if (!string.IsNullOrEmpty(isHotKeyClearWanted))
            SelfStateModel.IsHotKeyClearWanted = isHotKeyClearWanted == "1";

        if (!string.IsNullOrEmpty(isHotKeyFillAllAmmo))
            SelfStateModel.IsHotKeyFillAllAmmo = isHotKeyFillAllAmmo == "1";
        if (!string.IsNullOrEmpty(isHotKeyMovingFoward))
            SelfStateModel.IsHotKeyMovingFoward = isHotKeyMovingFoward == "1";

        if (!string.IsNullOrEmpty(isHotKeyNoCollision))
            SelfStateModel.IsHotKeyNoCollision = isHotKeyNoCollision == "1";
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {
        IsAppRunning = false;

        SaveConfig();
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    private void SaveConfig()
    {
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyToWaypoint", $"{Convert.ToInt32(SelfStateModel.IsHotKeyToWaypoint)}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyToObjective", $"{Convert.ToInt32(SelfStateModel.IsHotKeyToObjective)}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyFillHealthArmor", $"{Convert.ToInt32(SelfStateModel.IsHotKeyFillHealthArmor)}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyClearWanted", $"{Convert.ToInt32(SelfStateModel.IsHotKeyClearWanted)}");

        IniHelper.WriteValue("ExternalMenu", "IsHotKeyFillAllAmmo", $"{Convert.ToInt32(SelfStateModel.IsHotKeyFillAllAmmo)}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyMovingFoward", $"{Convert.ToInt32(SelfStateModel.IsHotKeyMovingFoward)}");

        IniHelper.WriteValue("ExternalMenu", "IsHotKeyNoCollision", $"{Convert.ToInt32(SelfStateModel.IsHotKeyNoCollision)}");
    }

    /// <summary>
    /// 全局热键 按键按下事件
    /// </summary>
    /// <param name="vK"></param>
    private void HotKeys_KeyDownEvent(WinVK vK)
    {
        switch (vK)
        {
            case WinVK.F3:
                if (SelfStateModel.IsHotKeyFillAllAmmo)
                {
                    Weapon.FillAllAmmo();
                }
                break;
            case WinVK.F4:
                if (SelfStateModel.IsHotKeyMovingFoward)
                {
                    Teleport.MoveFoward(MoveDistance);
                }
                break;
            case WinVK.F5:
                if (SelfStateModel.IsHotKeyToWaypoint)
                {
                    Teleport.ToWaypoint();
                }
                break;
            case WinVK.F6:
                if (SelfStateModel.IsHotKeyToObjective)
                {
                    Teleport.ToObjective();
                }
                break;
            case WinVK.F7:
                if (SelfStateModel.IsHotKeyFillHealthArmor)
                {
                    Player.FillHealth();
                    Player.FillArmor();
                }
                break;
            case WinVK.F8:
                if (SelfStateModel.IsHotKeyClearWanted)
                {
                    Player.WantedLevel(0x00);
                }
                break;
            case WinVK.Oem0:
                if (SelfStateModel.IsHotKeyNoCollision)
                {
                    Toggle_NoCollision = !Toggle_NoCollision;

                    Player.NoCollision(Toggle_NoCollision);
                    Setting.Player.NoCollision = Toggle_NoCollision;

                    if (Toggle_NoCollision)
                        Console.Beep(600, 75);
                    else
                        Console.Beep(500, 75);
                }
                break;
        }
    }

    private void SelfStateUpdateThread()
    {
        while (IsAppRunning)
        {
            long pCPedFactory = Memory.Read<long>(Pointers.WorldPTR);
            long pCPed = Memory.Read<long>(pCPedFactory + Offsets.CPed);
            long pCPlayerInfo = Memory.Read<long>(pCPed + Offsets.CPed_CPlayerInfo);

            float oHealth = Memory.Read<float>(pCPed + Offsets.CPed_Health);
            float oHealthMax = Memory.Read<float>(pCPed + Offsets.CPed_HealthMax);
            float oArmor = Memory.Read<float>(pCPed + Offsets.CPed_Armor);

            byte oWantedLevel = Memory.Read<byte>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_WantedLevel);
            float oWalkSpeed = Memory.Read<float>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_WalkSpeed);
            float oRunSpeed = Memory.Read<float>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_RunSpeed);
            float oSwimSpeed = Memory.Read<float>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_SwimSpeed);

            ////////////////////////////////////////////////////////////////

            this.Dispatcher.BeginInvoke(() =>
            {
                if (Slider_Health.Value != oHealth)
                    Slider_Health.Value = oHealth;

                if (Slider_HealthMax.Value != oHealthMax)
                    Slider_HealthMax.Value = oHealthMax;

                if (Slider_Armor.Value != oArmor)
                    Slider_Armor.Value = oArmor;

                if (Slider_WantedLevel.Value != oWantedLevel)
                    Slider_WantedLevel.Value = oWantedLevel;

                if (Slider_RunSpeed.Value != oRunSpeed)
                    Slider_RunSpeed.Value = oRunSpeed;

                if (Slider_SwimSpeed.Value != oSwimSpeed)
                    Slider_SwimSpeed.Value = oSwimSpeed;

                if (Slider_WalkSpeed.Value != oWalkSpeed)
                    Slider_WalkSpeed.Value = oWalkSpeed;
            });

            Thread.Sleep(1000);
        }
    }

    private void Slider_Health_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.Health((float)Slider_Health.Value);
    }

    private void Slider_HealthMax_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.HealthMax((float)Slider_HealthMax.Value);
    }

    private void Slider_Armor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.Armor((float)Slider_Armor.Value);
    }

    private void Slider_WantedLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.WantedLevel((byte)Slider_WantedLevel.Value);
    }

    private void Slider_RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.RunSpeed((float)Slider_RunSpeed.Value);
    }

    private void Slider_SwimSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.SwimSpeed((float)Slider_SwimSpeed.Value);
    }

    private void Slider_WalkSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.WalkSpeed((float)Slider_WalkSpeed.Value);
    }

    private void Slider_MoveDistance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        MoveDistance = (float)Slider_MoveDistance.Value;
    }

    private void CheckBox_PlayerGodMode_Click(object sender, RoutedEventArgs e)
    {
        Setting.Player.GodMode = CheckBox_PlayerGodMode.IsChecked == true;
        Player.GodMode(CheckBox_PlayerGodMode.IsChecked == true);
    }

    private void CheckBox_AntiAFK_Click(object sender, RoutedEventArgs e)
    {
        Setting.Player.AntiAFK = CheckBox_AntiAFK.IsChecked == true;
        Online.AntiAFK(CheckBox_AntiAFK.IsChecked == true);
    }

    private void CheckBox_NoRagdoll_Click(object sender, RoutedEventArgs e)
    {
        Setting.Player.NoRagdoll = CheckBox_NoRagdoll.IsChecked == true;
        Player.NoRagdoll(CheckBox_NoRagdoll.IsChecked == true);
    }

    private void CheckBox_UndeadOffRadar_Click(object sender, RoutedEventArgs e)
    {
        Player.UndeadOffRadar(CheckBox_UndeadOffRadar.IsChecked == true);
    }

    private void CheckBox_Invisibility_Click(object sender, RoutedEventArgs e)
    {
        Player.Invisible(CheckBox_Invisibility.IsChecked == true);
    }

    private void CheckBox_NPCIgnore_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_NPCIgnore.IsChecked == true && CheckBox_PoliceIgnore.IsChecked == false)
        {
            Player.NPCIgnore(0x040000);
        }
        else if (CheckBox_NPCIgnore.IsChecked == false && CheckBox_PoliceIgnore.IsChecked == true)
        {
            Player.NPCIgnore(0xC30000);
        }
        else if (CheckBox_NPCIgnore.IsChecked == true && CheckBox_PoliceIgnore.IsChecked == true)
        {
            Player.NPCIgnore(0xC70000);
        }
        else
        {
            Player.NPCIgnore(0x00);
        }
    }

    private void CheckBox_AutoClearWanted_Click(object sender, RoutedEventArgs e)
    {
        Player.WantedLevel(0x00);
        Setting.Auto.ClearWanted = CheckBox_AutoClearWanted.IsChecked == true;
    }

    private void CheckBox_AutoKillNPC_Click(object sender, RoutedEventArgs e)
    {
        World.KillAllNPC(false);
        Setting.Auto.KillNPC = CheckBox_AutoKillNPC.IsChecked == true;
    }

    private void CheckBox_AutoKillHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        World.KillAllNPC(true);
        Setting.Auto.KillHostilityNPC = CheckBox_AutoKillHostilityNPC.IsChecked == true;
    }

    private void CheckBox_AutoKillPolice_Click(object sender, RoutedEventArgs e)
    {
        World.KillAllPolice();
        Setting.Auto.KillPolice = CheckBox_AutoKillPolice.IsChecked == true;
    }

    private void Button_ToWaypoint_Click(object sender, RoutedEventArgs e)
    {
        Teleport.ToWaypoint();
    }

    private void Button_ToObjective_Click(object sender, RoutedEventArgs e)
    {
        Teleport.ToObjective();
    }

    private void Button_FillHealth_Click(object sender, RoutedEventArgs e)
    {
        Player.FillHealth();
    }

    private void Button_FillArmor_Click(object sender, RoutedEventArgs e)
    {
        Player.FillArmor();
    }

    private void Button_ClearWanted_Click(object sender, RoutedEventArgs e)
    {
        Player.WantedLevel(0x00);
    }

    private void Button_Suicide_Click(object sender, RoutedEventArgs e)
    {
        Player.Suicide();
    }

    private void CheckBox_ProofBullet_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofBullet(CheckBox_ProofBullet.IsChecked == true);
    }

    private void CheckBox_ProofFire_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofFire(CheckBox_ProofFire.IsChecked == true);
    }

    private void CheckBox_ProofCollision_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofCollision(CheckBox_ProofCollision.IsChecked == true);
    }

    private void CheckBox_ProofMelee_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofMelee(CheckBox_ProofMelee.IsChecked == true);
    }

    private void CheckBox_ProofExplosion_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofExplosion(CheckBox_ProofExplosion.IsChecked == true);
    }

    private void CheckBox_ProofSteam_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofSteam(CheckBox_ProofSteam.IsChecked == true);
    }

    private void CheckBox_ProofDrown_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofDrown(CheckBox_ProofDrown.IsChecked == true);
    }

    private void CheckBox_ProofWater_Click(object sender, RoutedEventArgs e)
    {
        Player.ProofWater(CheckBox_ProofWater.IsChecked == true);
    }

    private void CheckBox_NoCollision_Click(object sender, RoutedEventArgs e)
    {
        Toggle_NoCollision = SelfStateModel.IsHotKeyNoCollision;

        Player.NoCollision(Toggle_NoCollision);
        Setting.Player.NoCollision = Toggle_NoCollision;
    }

    private void Button_ToWaypoint_Super_Click(object sender, RoutedEventArgs e)
    {
        Teleport.ToWaypointSuper();
    }
}
