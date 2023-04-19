namespace GTA5Core.RAGE.Stats;

public static class StatData
{
    public class StatClass
    {
        public string Name { get; set; }
        public List<StatInfo> StatInfos { get; set; }
    }

    public class StatInfo
    {
        public string Hash { get; set; }
        public int Value { get; set; }
    }

   
}
