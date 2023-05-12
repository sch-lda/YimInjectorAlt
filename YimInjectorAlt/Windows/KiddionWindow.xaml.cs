using GTA5Shared.Helper;

using CommunityToolkit.Mvvm.Input;

namespace YimInjectorAlt.Windows;

/// <summary>
/// KiddionWindow.xaml 的交互逻辑
/// </summary>
public partial class KiddionWindow
{
    public KiddionWindow()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    private void Window_Kiddion_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void Window_Kiddion_Closing(object sender, CancelEventArgs e)
    {

    }

    /// <summary>
    /// Lua脚本按钮点击
    /// </summary>
    /// <param name="Name"></param>
    [RelayCommand]
    private void LuaScriptClick(string Name)
    {
        try
        {
           //

            switch (Name)
            {
                case "LuaScriptDir":
                    ProcessHelper.OpenLink(FileHelper.Dir_Kiddion_Scripts);
                    break;
                case "ClearScriptDir":
                    ClearScriptDirClick();
                    break;
                case "AliceLua":
                    AliceLuaClick();
                    break;
                case "IdealLua":
                    IdealLuaClick();
                    break;
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 是否清空脚本目录
    /// </summary>
    private void ClearScriptDirClick()
    {
        ProcessHelper.CloseProcess("Kiddion");
        FileHelper.ClearDirectory(FileHelper.Dir_Kiddion_Scripts);

        FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Scripts_Readme, FileHelper.File_Kiddion_Scripts_Readme);
        NotifierHelper.Show(NotifierType.Success, "清空Kiddion Lua脚本文件夹成功");
    }

    /// <summary>
    /// 释放Lua脚本
    /// </summary>
    private void ReleaseLua(string luaName)
    {
        var lua = $"{FileHelper.ResFiles}.Kiddion.scripts.{luaName}.zip";
        var file = $"{FileHelper.Dir_Kiddion_Scripts}\\{luaName}.zip";

        FileHelper.ExtractResFile(lua, file);

        using var archive = ZipFile.OpenRead(file);
        archive.ExtractToDirectory(FileHelper.Dir_Kiddion_Scripts);
        archive.Dispose();

        File.Delete(file);
        NotifierHelper.Show(NotifierType.Success, "脚本替换成功，请重新启动Kiddion查看");
    }

    /// <summary>
    /// 释放 Alice Lua脚本
    /// </summary>
    private void AliceLuaClick()
    {
        ReleaseLua("AliceLua");
    }

    /// <summary>
    /// 释放 Ideal Lua脚本
    /// </summary>
    private void IdealLuaClick()
    {
        ReleaseLua("IdealLua");
    }
}
