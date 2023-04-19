using CommunityToolkit.Mvvm.ComponentModel;

namespace YimInjectorAlt.Models;

public class HacksModel : ObservableObject
{
    private bool kiddionIsRun;
    /// <summary>
    /// Kiddion运行状态
    /// </summary>
    public bool KiddionIsRun
    {
        get => kiddionIsRun;
        set => SetProperty(ref kiddionIsRun, value);
    }

    private bool gTAHaxIsRun;
    /// <summary>
    /// GTAHax运行状态
    /// </summary>
    public bool GTAHaxIsRun
    {
        get => gTAHaxIsRun;
        set => SetProperty(ref gTAHaxIsRun, value);
    }

    private bool bincoHaxIsRun;
    /// <summary>
    /// BincoHax运行状态
    /// </summary>
    public bool BincoHaxIsRun
    {
        get => bincoHaxIsRun;
        set => SetProperty(ref bincoHaxIsRun, value);
    }

    private bool lSCHaxIsRun;
    /// <summary>
    /// LSCHax运行状态
    /// </summary>
    public bool LSCHaxIsRun
    {
        get => lSCHaxIsRun;
        set => SetProperty(ref lSCHaxIsRun, value);
    }
}
