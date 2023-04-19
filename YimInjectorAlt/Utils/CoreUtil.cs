using YimInjectorAlt.Data;

namespace YimInjectorAlt.Utils;

public static class CoreUtil
{
    /// <summary>
    /// 主窗口标题
    /// </summary>
    public const string MainAppWindowName = "GTA5线上小助手-仅测试 支持1.66 完全免费 v";

    /// <summary>
    /// 程序服务端版本号，如：1.2.3.4
    /// </summary>
    public static Version ServerVersion = Version.Parse("0.0.0.0");

    /// <summary>
    /// 程序客户端版本号，如：1.2.3.4
    /// </summary>
    public static readonly Version ClientVersion = Application.ResourceAssembly.GetName().Version;

    /// <summary>
    /// 程序客户端最后编译时间
    /// </summary>
    public static readonly DateTime BuildDate = File.GetLastWriteTime(Environment.ProcessPath);

    /// <summary>
    /// 检查更新相关信息
    /// </summary>
    public static UpdateInfo UpdateInfo { get; set; }

    /// <summary>
    /// 正在更新时的文件名
    /// </summary>
    public const string HalfwayAppName = "未下载完的小助手更新文件.exe";

    /// <summary>
    /// 固定下载更新地址
    /// </summary>
    public static string UpdateAddress = "https://github.com/CrazyZhang666/GTA5/releases/download/update/YimInjectorAlt.exe";

    /// <summary>
    /// 更新完成后的完整文件名
    /// </summary>
    /// <returns></returns>
    public static string FullAppName()
    {
        return $"{MainAppWindowName}{ServerVersion}.exe";
    }

    /// <summary>
    /// 计算时间差，即软件运行时间
    /// </summary>
    public static string ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
    {
        var ts1 = new TimeSpan(dateBegin.Ticks);
        var ts2 = new TimeSpan(dateEnd.Ticks);

        return ts1.Subtract(ts2).Duration().ToString("c")[..8];
    }
}
