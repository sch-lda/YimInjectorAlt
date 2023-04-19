namespace GTA5Core.RAGE.Teleports;

public class TeleportData
{
    public static readonly List<TeleportInfo> Custom = new()
    {

    };

    public static readonly List<TeleportInfo> Common = new()
    {

    };

    public static readonly List<TeleportInfo> Indoor = new()
    {
      
    };

    public static readonly List<TeleportInfo> Mission = new()
    {
      
    };

    public static readonly List<TeleportClass> TeleportClasses = new()
    {
      
    };
}

public class TeleportClass
{
    public string Icon { get; set; }
    public string Name { get; set; }
    public List<TeleportInfo> TeleportInfos { get; set; }
}

public class TeleportInfo
{
    public string Name { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}
