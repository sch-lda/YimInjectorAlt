using CommunityToolkit.Mvvm.ComponentModel;

namespace YimInjectorAlt.Models;

public class MainModel : ObservableObject
{
    private bool isGTA5Run;
    /// <summary>
    /// GTA5是否运行
    /// </summary>
    public bool IsGTA5Run
    {
        get => isGTA5Run;
        set => SetProperty(ref isGTA5Run, value);
    }

    private Version appVersion;
    /// <summary>
    /// 程序版本号
    /// </summary>
    public Version AppVersion
    {
        get => appVersion;
        set => SetProperty(ref appVersion, value);
    }

    private string appRunTime;
    /// <summary>
    /// 程序运行时间
    /// </summary>
    public string AppRunTime
    {
        get => appRunTime;
        set => SetProperty(ref appRunTime, value);
    }
}
