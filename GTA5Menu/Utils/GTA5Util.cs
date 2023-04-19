using GTA5Shared.Helper;

namespace GTA5Menu.Utils;

public static class GTA5Util
{
    public static string File_Config_Teleports { get; private set; }
    public static string File_Config_Vehicles { get; private set; }
    public static string File_Config_Blips { get; private set; }

    static GTA5Util()
    {
        File_Config_Teleports = Path.Combine(FileHelper.Dir_Config, "Teleports.json");
        File_Config_Vehicles = Path.Combine(FileHelper.Dir_Config, "Vehicles.json");
        File_Config_Blips = Path.Combine(FileHelper.Dir_Config, "Blips.json");
    }
}
