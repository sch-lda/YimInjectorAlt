namespace GTA5Core.RAGE.Vehicles;

public static class VehicleWheel
{
    public class WheelClass
    {
        public string Name { get; set; }
        public List<WheelInfo> WheelInfos { get; set; }
    }

    public class WheelInfo
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    /// <summary>
    /// Sport 运动跑车
    /// </summary>
    public static readonly List<WheelInfo> Sport = new()
    {
        new WheelInfo(){ Name="Inferno", Value=1 },
        new WheelInfo(){ Name="Deep Five", Value=2 },
        new WheelInfo(){ Name="Lozspeed Mk.V", Value=3 },
        new WheelInfo(){ Name="Diamond Cut", Value=4 },
        new WheelInfo(){ Name="Chrono", Value=5 },
        new WheelInfo(){ Name="Feroci RR", Value=6 },
        new WheelInfo(){ Name="FiftyNine", Value=7 },
        new WheelInfo(){ Name="Mercie", Value=8 },
        new WheelInfo(){ Name="Synthetic Z", Value=9 },
        new WheelInfo(){ Name="Organic Type O", Value=10 },
        new WheelInfo(){ Name="Endo v.1", Value=11 },
        new WheelInfo(){ Name="GT One", Value=12 },
        new WheelInfo(){ Name="Duper 7", Value=13 },
        new WheelInfo(){ Name="Uzer", Value=14 },
        new WheelInfo(){ Name="GroundRide", Value=15 },
        new WheelInfo(){ Name="S Racer", Value=16 },
        new WheelInfo(){ Name="Venum", Value=17 },
        new WheelInfo(){ Name="Cosmo", Value=18 },
        new WheelInfo(){ Name="Dash VIP", Value=19 },
        new WheelInfo(){ Name="Ice Kid", Value=20 },
        new WheelInfo(){ Name="Ruff Weld", Value=21 },
        new WheelInfo(){ Name="Wangan Master", Value=22 },
        new WheelInfo(){ Name="Super Five", Value=23 },
        new WheelInfo(){ Name="Endo v.2", Value=24 },
        new WheelInfo(){ Name="Split Six", Value=25 },
    };

    /// <summary>
    /// Muscle 肌肉车
    /// </summary>
    public static readonly List<WheelInfo> Muscle = new()
    {
        new WheelInfo(){ Name="Classic Five", Value=1 },
        new WheelInfo(){ Name="Dukes", Value=2 },
        new WheelInfo(){ Name="Muscle Freak", Value=3 },
        new WheelInfo(){ Name="Kracka", Value=4 },
        new WheelInfo(){ Name="Azreal", Value=5 },
        new WheelInfo(){ Name="Mecha", Value=6 },
        new WheelInfo(){ Name="Black Top", Value=7 },
        new WheelInfo(){ Name="Drag SPL", Value=8 },
        new WheelInfo(){ Name="Revolver", Value=9 },
        new WheelInfo(){ Name="Classic Rod", Value=10 },
        new WheelInfo(){ Name="Fairlie", Value=11 },
    };

    /// <summary>
    /// Lowrider 低底盘车
    /// </summary>
    public static readonly List<WheelInfo> Lowrider = new()
    {
        new WheelInfo(){ Name="Flare", Value=1 },
        new WheelInfo(){ Name="Wired", Value=2 },
        new WheelInfo(){ Name="Triple Golds", Value=3 },
        new WheelInfo(){ Name="Big Worm", Value=4 },
        new WheelInfo(){ Name="Seven Fives", Value=5 },
        new WheelInfo(){ Name="Split Six", Value=6 },
        new WheelInfo(){ Name="Fresh Mesh", Value=7 },
        new WheelInfo(){ Name="Lead Sled", Value=8 },
        new WheelInfo(){ Name="Turbine", Value=9 },
        new WheelInfo(){ Name="Super Fin", Value=10 },
        new WheelInfo(){ Name="Classic Rod", Value=11 },
        new WheelInfo(){ Name="Dollar", Value=12 },
        new WheelInfo(){ Name="Dukes", Value=13 },
        new WheelInfo(){ Name="Low Five", Value=14 },
        new WheelInfo(){ Name="Gooch", Value=15 },
    };

    /// <summary>
    /// SUV
    /// </summary>
    public static readonly List<WheelInfo> SUV = new()
    {
        new WheelInfo(){ Name="VIP", Value=1 },
        new WheelInfo(){ Name="Benefactor", Value=2 },
        new WheelInfo(){ Name="Cosmo", Value=3 },
        new WheelInfo(){ Name="Bippu", Value=4 },
        new WheelInfo(){ Name="Royal Six", Value=5 },
        new WheelInfo(){ Name="Fagorme", Value=6 },
        new WheelInfo(){ Name="Deluxe", Value=7 },
        new WheelInfo(){ Name="Iced Out", Value=8 },
        new WheelInfo(){ Name="Cognescenti", Value=9 },
        new WheelInfo(){ Name="LozSpeed Ten", Value=10 },
        new WheelInfo(){ Name="Supernova", Value=11 },
        new WheelInfo(){ Name="Obey RS", Value=12 },
        new WheelInfo(){ Name="LozSpeed Baller", Value=13 },
        new WheelInfo(){ Name="Extravaganzo", Value=14 },
        new WheelInfo(){ Name="Split Six", Value=15 },
        new WheelInfo(){ Name="Empowered", Value=16 },
        new WheelInfo(){ Name="Sunrise", Value=17 },
        new WheelInfo(){ Name="Dash VIP", Value=18 },
        new WheelInfo(){ Name="Cutter", Value=19 },
    };

    /// <summary>
    /// Offroad 越野车
    /// </summary>
    public static readonly List<WheelInfo> Offroad = new()
    {
        new WheelInfo(){ Name="Raider", Value=1 },
        new WheelInfo(){ Name="Mudslinger", Value=2 },
        new WheelInfo(){ Name="Nevis", Value=3 },
        new WheelInfo(){ Name="Cairngorm", Value=4 },
        new WheelInfo(){ Name="Amazon", Value=5 },
        new WheelInfo(){ Name="Challenger", Value=6 },
        new WheelInfo(){ Name="Dune Basher", Value=7 },
        new WheelInfo(){ Name="Five Star", Value=8 },
        new WheelInfo(){ Name="Rock Crawler", Value=9 },
        new WheelInfo(){ Name="Mil Spec Steelie", Value=10 },
    };

    /// <summary>
    /// Tuner
    /// </summary>
    public static readonly List<WheelInfo> Tuner = new()
    {
        new WheelInfo(){ Name="Cosmo", Value=1 },
        new WheelInfo(){ Name="Super Mesh", Value=2 },
        new WheelInfo(){ Name="Outsider", Value=3 },
        new WheelInfo(){ Name="Rollas", Value=4 },
        new WheelInfo(){ Name="Driftmeister", Value=5 },
        new WheelInfo(){ Name="Slicer", Value=6 },
        new WheelInfo(){ Name="El Quattro", Value=7 },
        new WheelInfo(){ Name="Dubbed", Value=8 },
        new WheelInfo(){ Name="Five Star", Value=9 },
        new WheelInfo(){ Name="Sideways", Value=10 },
        new WheelInfo(){ Name="Apex", Value=11 },
        new WheelInfo(){ Name="Stanced EG", Value=12 },
        new WheelInfo(){ Name="Countersteer", Value=13 },
        new WheelInfo(){ Name="Endo v.1", Value=14 },
        new WheelInfo(){ Name="Endo v.2 Dish", Value=15 },
        new WheelInfo(){ Name="Gruppe Z", Value=16 },
        new WheelInfo(){ Name="Choku Dori", Value=17 },
        new WheelInfo(){ Name="Chicane", Value=18 },
        new WheelInfo(){ Name="Saisoku", Value=19 },
        new WheelInfo(){ Name="Dished Eight", Value=20 },
        new WheelInfo(){ Name="Fujiwara", Value=21 },
        new WheelInfo(){ Name="Zokusha", Value=22 },
        new WheelInfo(){ Name="Battle VIII", Value=23 },
        new WheelInfo(){ Name="Rally Master", Value=24 },
    };

    /// <summary>
    /// Bike Wheels
    /// </summary>
    public static readonly List<WheelInfo> Bike = new()
    {
        new WheelInfo(){ Name="Speedway", Value=1 },
        new WheelInfo(){ Name="Street Special", Value=2 },
        new WheelInfo(){ Name="Racer", Value=3 },
        new WheelInfo(){ Name="Track Star", Value=4 },
        new WheelInfo(){ Name="Overlord", Value=5 },
        new WheelInfo(){ Name="Trident", Value=6 },
        new WheelInfo(){ Name="Triple Threat", Value=7 },
        new WheelInfo(){ Name="Stiletto", Value=8 },
        new WheelInfo(){ Name="Wires", Value=9 },
        new WheelInfo(){ Name="Bobber", Value=10 },
        new WheelInfo(){ Name="Solidus", Value=11 },
        new WheelInfo(){ Name="Ice Shield", Value=12 },
        new WheelInfo(){ Name="Loops", Value=13 },
    };

    /// <summary>
    /// High End 高档车
    /// </summary>
    public static readonly List<WheelInfo> High = new()
    {
        new WheelInfo(){ Name="Shadow", Value=1 },
        new WheelInfo(){ Name="Hypher", Value=2 },
        new WheelInfo(){ Name="Blade", Value=3 },
        new WheelInfo(){ Name="Diamond", Value=4 },
        new WheelInfo(){ Name="Supa Gee", Value=5 },
        new WheelInfo(){ Name="Chromatic Z", Value=6 },
        new WheelInfo(){ Name="Mercie Ch.Lip", Value=7 },
        new WheelInfo(){ Name="Obey RS", Value=8 },
        new WheelInfo(){ Name="GT Chrome", Value=9 },
        new WheelInfo(){ Name="Cheetah RR", Value=10 },
        new WheelInfo(){ Name="Solar", Value=11 },
        new WheelInfo(){ Name="Split Ten", Value=12 },
        new WheelInfo(){ Name="Dash VIP", Value=13 },
        new WheelInfo(){ Name="LozSpeed Ten", Value=14 },
        new WheelInfo(){ Name="Carbon Inferno", Value=15 },
        new WheelInfo(){ Name="Carbon Shadow", Value=16 },
        new WheelInfo(){ Name="Carbonic Z", Value=17 },
        new WheelInfo(){ Name="Carbon Solar", Value=18 },
        new WheelInfo(){ Name="Cheetah Carbon R", Value=19 },
        new WheelInfo(){ Name="Carbon S Racer", Value=20 },
    };

    /// <summary>
    /// Benny's Original 班尼原厂
    /// </summary>
    public static readonly List<WheelInfo> Original = new()
    {
        new WheelInfo(){ Name="OG Hunnets", Value=1 },
        new WheelInfo(){ Name="OG Hunnets (Chrome)", Value=2 },
        new WheelInfo(){ Name="Knock-Offs", Value=3 },
        new WheelInfo(){ Name="Knock-Offs (Chrome)", Value=4 },
        new WheelInfo(){ Name="Spoked Out", Value=5 },
        new WheelInfo(){ Name="Spoked Out (Chrome)", Value=6 },
        new WheelInfo(){ Name="Vintage Wire", Value=7 },
        new WheelInfo(){ Name="Vintage Wire (Chrome)", Value=8 },
        new WheelInfo(){ Name="Smoothie", Value=9 },
        new WheelInfo(){ Name="Smoothie (Chrome)", Value=10 },
        new WheelInfo(){ Name="Smoothie (Solid)", Value=11 },
        new WheelInfo(){ Name="Rod Me Up", Value=12 },
        new WheelInfo(){ Name="Rod Me Up (Chrome)", Value=13 },
        new WheelInfo(){ Name="Rod Me Up (Solid)", Value=14 },
        new WheelInfo(){ Name="Clean", Value=15 },
        new WheelInfo(){ Name="Lotta Chrome", Value=16 },
        new WheelInfo(){ Name="Spindles", Value=17 },
        new WheelInfo(){ Name="Viking", Value=18 },
        new WheelInfo(){ Name="Triple Spoke", Value=19 },
        new WheelInfo(){ Name="Pharohe", Value=20 },
        new WheelInfo(){ Name="Tiger Style", Value=21 },
        new WheelInfo(){ Name="Three Wheelin", Value=22 },
        new WheelInfo(){ Name="Big Bar", Value=23 },
        new WheelInfo(){ Name="Hiohazard", Value=24 },
        new WheelInfo(){ Name="Waves", Value=25 },
        new WheelInfo(){ Name="lick Lick", Value=26 },
        new WheelInfo(){ Name="Spiralizer", Value=27 },
        new WheelInfo(){ Name="Hypnotics", Value=28 },
        new WheelInfo(){ Name="Psycho-Delic", Value=29 },
        new WheelInfo(){ Name="Half Cut", Value=30 },
        new WheelInfo(){ Name="Super Electric", Value=31 },
    };

    /// <summary>
    /// Benny's Bespoke 班尼定制
    /// </summary>
    public static readonly List<WheelInfo> Bespoke = new()
    {
        new WheelInfo(){ Name="Chrome OG Hunnets", Value=1 },
        new WheelInfo(){ Name="Gold OG Hunnets", Value=2 },
        new WheelInfo(){ Name="Chrome Wires", Value=3 },
        new WheelInfo(){ Name="Gold Wires", Value=4 },
        new WheelInfo(){ Name="Chrome Spoked Out", Value=5 },
        new WheelInfo(){ Name="Gold Spoked Out", Value=6 },
        new WheelInfo(){ Name="Chrome Knock-Offs", Value=7 },
        new WheelInfo(){ Name="Gold Knock-Offs", Value=8 },
        new WheelInfo(){ Name="Chrome Bigger Worm", Value=9 },
        new WheelInfo(){ Name="Gold Bigger Worm", Value=10 },
        new WheelInfo(){ Name="Chrome Vintage Wire", Value=11 },
        new WheelInfo(){ Name="Gold Vintage Wire", Value=12 },
        new WheelInfo(){ Name="Chrome Classic Wire", Value=13 },
        new WheelInfo(){ Name="Gold Classic Wire", Value=14 },
        new WheelInfo(){ Name="Chrome Smoothie", Value=15 },
        new WheelInfo(){ Name="Gold Smoothie", Value=16 },
        new WheelInfo(){ Name="Chrome Classic Rod", Value=17 },
        new WheelInfo(){ Name="Gold Classic Rod", Value=18 },
        new WheelInfo(){ Name="Chrome Dollar", Value=19 },
        new WheelInfo(){ Name="Gold Dollar", Value=20 },
        new WheelInfo(){ Name="Chrome Mighty Star", Value=21 },
        new WheelInfo(){ Name="Gold Mighty Star", Value=22 },
        new WheelInfo(){ Name="Chrome Decadent Dish", Value=23 },
        new WheelInfo(){ Name="Gold Decadent Dish", Value=24 },
        new WheelInfo(){ Name="Chrome Razor Style", Value=25 },
        new WheelInfo(){ Name="Gold Razor Style", Value=26 },
        new WheelInfo(){ Name="Chrome Celtic Knot", Value=27 },
        new WheelInfo(){ Name="Gold Celtic Knot", Value=28 },
        new WheelInfo(){ Name="Chrome Warrior Dish", Value=29 },
        new WheelInfo(){ Name="Gold Warrior Dish", Value=30 },
        new WheelInfo(){ Name="Gold Big Dog Spokes", Value=31 },
    };

    /// <summary>
    /// Race (Formula)
    /// </summary>
    public static readonly List<WheelInfo> Race = new()
    {
        new WheelInfo(){ Name="Classic 5", Value=1 },
        new WheelInfo(){ Name="Classic 5 (Striped)", Value=2 },
        new WheelInfo(){ Name="Retro Star", Value=3 },
        new WheelInfo(){ Name="Retro Star (Striped)", Value=4 },
        new WheelInfo(){ Name="Triplex", Value=5 },
        new WheelInfo(){ Name="Triplex (Striped)", Value=6 },
        new WheelInfo(){ Name="70s Spec", Value=7 },
        new WheelInfo(){ Name="70s Spec (Striped)", Value=8 },
        new WheelInfo(){ Name="Super 5R", Value=9 },
        new WheelInfo(){ Name="Super 5R (Striped)", Value=10 },
        new WheelInfo(){ Name="Speedster", Value=11 },
        new WheelInfo(){ Name="Speedster (Striped)", Value=12 },
        new WheelInfo(){ Name="GP-90", Value=13 },
        new WheelInfo(){ Name="GP-90 (Striped)", Value=14 },
        new WheelInfo(){ Name="Superspoke", Value=15 },
        new WheelInfo(){ Name="Superspoke (Striped)", Value=16 },
        new WheelInfo(){ Name="Gridline", Value=17 },
        new WheelInfo(){ Name="Gridline (Striped)", Value=18 },
        new WheelInfo(){ Name="Snowflake", Value=19 },
        new WheelInfo(){ Name="Snowflake (Striped)", Value=20 },
    };

    /// <summary>
    /// Street 街头
    /// </summary>
    public static readonly List<WheelInfo> Street = new()
    {
        new WheelInfo(){ Name="Retro Steelie", Value=1 },
        new WheelInfo(){ Name="Poverty Spec Steelie", Value=2 },
        new WheelInfo(){ Name="Concave Steelie", Value=3 },
        new WheelInfo(){ Name="Nebula", Value=4 },
        new WheelInfo(){ Name="Hotring Steelie", Value=5 },
        new WheelInfo(){ Name="Cup Champion", Value=6 },
        new WheelInfo(){ Name="Stanced EG Custom", Value=7 },
        new WheelInfo(){ Name="Kracka Custom", Value=8 },
        new WheelInfo(){ Name="Dukes Custom", Value=9 },
        new WheelInfo(){ Name="Endo v.3 Custom", Value=10 },
        new WheelInfo(){ Name="V8 Killer", Value=11 },
        new WheelInfo(){ Name="Fujiwara Custom", Value=12 },
        new WheelInfo(){ Name="Cosmo MK2", Value=13 },
        new WheelInfo(){ Name="Aero Star", Value=14 },
        new WheelInfo(){ Name="Hype Five", Value=15 },
        new WheelInfo(){ Name="Ruff Weld Mega Deep", Value=16 },
        new WheelInfo(){ Name="Mercie Concave", Value=17 },
        new WheelInfo(){ Name="Sugoi Concave", Value=18 },
        new WheelInfo(){ Name="Synthetic Z Concave", Value=19 },
        new WheelInfo(){ Name="Endo v.4 Dished", Value=20 },
        new WheelInfo(){ Name="Hyperfresh", Value=21 },
        new WheelInfo(){ Name="Truffade Concave", Value=22 },
        new WheelInfo(){ Name="Organic Type 2", Value=23 },
        new WheelInfo(){ Name="Big Mamba", Value=24 },
        new WheelInfo(){ Name="Deep Flake", Value=25 },
        new WheelInfo(){ Name="Cosmo MK3", Value=26 },
        new WheelInfo(){ Name="Concave Racer", Value=27 },
        new WheelInfo(){ Name="Deep Flake Reverse", Value=28 },
        new WheelInfo(){ Name="Wild Wagon", Value=29 },
        new WheelInfo(){ Name="Concave Mega Mesh", Value=30 },
    };

    /// <summary>
    /// Track 赛道
    /// </summary>
    public static readonly List<WheelInfo> Track = new()
    {
        new WheelInfo(){ Name="Rally Throwback", Value=1 },
        new WheelInfo(){ Name="Gravel Trap", Value=2 },
        new WheelInfo(){ Name="Stove Top", Value=3 },
        new WheelInfo(){ Name="Stove Top Mesh", Value=4 },
        new WheelInfo(){ Name="Retro 3 Piece", Value=5 },
        new WheelInfo(){ Name="Rally Monoblock", Value=6 },
        new WheelInfo(){ Name="Forged 5", Value=7 },
        new WheelInfo(){ Name="Split Star", Value=8 },
        new WheelInfo(){ Name="Speed Boy", Value=9 },
        new WheelInfo(){ Name="90s Running", Value=10 },
        new WheelInfo(){ Name="Tropos", Value=11 },
        new WheelInfo(){ Name="Exos", Value=12 },
        new WheelInfo(){ Name="High Five", Value=13 },
        new WheelInfo(){ Name="Super Luxe", Value=14 },
        new WheelInfo(){ Name="Pure Business", Value=15 },
        new WheelInfo(){ Name="Pepper Pot", Value=16 },
        new WheelInfo(){ Name="BlackTop Blender", Value=17 },
        new WheelInfo(){ Name="Throwback", Value=18 },
        new WheelInfo(){ Name="Expressway", Value=19 },
        new WheelInfo(){ Name="Hidden Six", Value=20 },
        new WheelInfo(){ Name="Dinka SPL", Value=21 },
        new WheelInfo(){ Name="Retro Turbofan", Value=22 },
        new WheelInfo(){ Name="Conical Turbofan", Value=23 },
        new WheelInfo(){ Name="Ice Storm", Value=24 },
        new WheelInfo(){ Name="Super Turbine", Value=25 },
        new WheelInfo(){ Name="Modern Mash", Value=26 },
        new WheelInfo(){ Name="Forged Star", Value=27 },
        new WheelInfo(){ Name="Showflake", Value=28 },
        new WheelInfo(){ Name="Giga Mesh", Value=29 },
        new WheelInfo(){ Name="Mesh Meister", Value=30 },
    };

    /// <summary>
    /// 车轮分类列表
    /// </summary>
    public static readonly List<WheelClass> WheelClasses = new()
    {
        new WheelClass(){ Name="Sport", WheelInfos=Sport },
        new WheelClass(){ Name="Muscle", WheelInfos=Muscle },
        new WheelClass(){ Name="Lowrider", WheelInfos=Lowrider },
        new WheelClass(){ Name="SUV", WheelInfos=SUV },
        new WheelClass(){ Name="Offroad", WheelInfos=Offroad },
        new WheelClass(){ Name="Tuner", WheelInfos=Tuner },
        new WheelClass(){ Name="Bike Wheels", WheelInfos=Bike },
        new WheelClass(){ Name="High End", WheelInfos=High },
        new WheelClass(){ Name="Benny's Original", WheelInfos=Original },
        new WheelClass(){ Name="Benny's Bespoke", WheelInfos=Bespoke },
        new WheelClass(){ Name="Race (Formula)", WheelInfos=Race },
        new WheelClass(){ Name="Street", WheelInfos=Street },
        new WheelClass(){ Name="Track", WheelInfos=Track },
    };
}
