namespace GTA5Core.RAGE.Onlines;

public static class OnlineData
{
    public class OnlineInfo
    {
        public string Name;
        public int Value;
    }

    public static List<OnlineInfo> Blips = new()
    {
   
    };

    public static List<OnlineInfo> Sessions = new()
    {
       
    };

    public static List<OnlineInfo> RPxNs = new()
    {
      
    };

    public static List<OnlineInfo> REPxNs = new()
    {
      
    };

    // Set Global_1946791 to 1, otherwise you'll see regular crates.
    // Set Global_1946637 to one of these: 2, 4, 6, 7, 8, 9.
    // Now you'll see the unique cargo in your terrorbyte.

    public static List<OnlineInfo> CEOCargos = new()
    {
       
    };

    public static List<OnlineInfo> CEOSpecialCargos = new()
    {
       
    };

    public static List<OnlineInfo> MerryWeatherServices = new()
    {
      
    };

    public static List<OnlineInfo> LocalWeathers = new()
    {
     
    };

    public static List<OnlineInfo> ImpactExplosions = new()
    {
      
    };

    public static List<OnlineInfo> VehicleExtras = new()
    {
       
    };
}
