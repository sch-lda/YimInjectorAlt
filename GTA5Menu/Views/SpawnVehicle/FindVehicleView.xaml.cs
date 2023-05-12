using GTA5Menu.Data;

using GTA5Core.RAGE;
using GTA5Core.RAGE.Vehicles;
using GTA5Core.Feature;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.SpawnVehicle;

/// <summary>
/// FindVehicleView.xaml 的交互逻辑
/// </summary>
public partial class FindVehicleView : UserControl
{
    public List<ModelInfo> AllVehicles { get; private set; } = new();
    public ObservableCollection<ModelInfo> FindVehicles { get; private set; } = new();

    public FindVehicleView()
    {
        InitializeComponent();
        this.DataContext = this;
        SpawnVehicleWindow.WindowClosingEvent += SpawnVehicleWindow_WindowClosingEvent;

        // 填充全部载具
        foreach (var info in VehicleHash.AllVehicles)
        {
            AllVehicles.Add(new()
            {
                Class = info.Class,
                Name = info.Name,
                Value = info.Value,
                Image = info.Image,
                Mod = info.Mod
            });
        }
    }

    private void SpawnVehicleWindow_WindowClosingEvent()
    {

    }

    private void FindVehiclesByModeName()
    {
        if (FindVehicles.Count != 0)
            FindVehicles.Clear();

        var name = TextBox_ModelName.Text;
        if (string.IsNullOrWhiteSpace(name))
            return;

        var result = AllVehicles.FindAll(v => v.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));
        if (result.Count == 0)
            return;

        result.ForEach(v => FindVehicles.Add(v));
    }

    private void TextBox_ModelName_TextChanged(object sender, TextChangedEventArgs e)
    {
        FindVehiclesByModeName();
    }

    private async void MenuItem_SpawnVehicleA_Click(object sender, RoutedEventArgs e)
    {
       //

        if (ListBox_VehicleInfo.SelectedItem is ModelInfo info)
        {
            await Vehicle2.SpawnVehicle(info.Value, -255.0f, 5, info.Mod);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的载具，操作取消");
        }
    }

    private async void MenuItem_SpawnVehicleB_Click(object sender, RoutedEventArgs e)
    {
       //

        if (ListBox_VehicleInfo.SelectedItem is ModelInfo info)
        {
            await Vehicle2.SpawnVehicle(info.Value, 0.0f, 5, info.Mod);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的载具，操作取消");
        }
    }

    private void MenuItem_AddMyFavorite_Click(object sender, RoutedEventArgs e)
    {
       //

        if (ListBox_VehicleInfo.SelectedItem is ModelInfo info)
        {
            MyVehicleView.ActionAddMyFavorite(info);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的载具，操作取消");
        }
    }

    private void ListBox_VehicleInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        MenuItem_SpawnVehicleA_Click(null, null);
    }
}
