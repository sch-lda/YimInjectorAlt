﻿namespace GTA5Shared.Helper;

public static class ProcessHelper
{
    /// <summary>
    /// 判断程序是否运行
    /// </summary>
    /// <param name="appName">程序名称</param>
    /// <returns>正在运行返回true，未运行返回false</returns>
    public static bool IsAppRun(string appName)
    {
        return Process.GetProcessesByName(appName).Length > 0;
    }

    /// <summary>
    /// 判断GTA5程序是否运行
    /// </summary>
    /// <returns>正在运行返回true，未运行返回false</returns>
    public static bool IsGTA5Run()
    {
        var pArray = Process.GetProcessesByName("GTA5");
        if (pArray.Length > 0)
        {
            foreach (var item in pArray)
            {
                if (item.MainWindowHandle == IntPtr.Zero)
                    continue;

                if (item.MainModule.FileVersionInfo.LegalCopyright.Contains("Rockstar Games Inc."))
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 打开http链接或者文件夹路径
    /// </summary>
    /// <param name="url"></param>
    public static void OpenLink(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 打开指定进程，可以附带运行参数
    /// </summary>
    /// <param name="path">本地文件夹路径</param>
    public static void OpenProcess(string path, string args = "")
    {
        try
        {
            Process.Start(path, args);
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 以管理员权限打开指定程序
    /// </summary>
    /// <param name="path">程序路径</param>
    public static void OpenProcessWithWorkDir(string path)
    {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
        OpenProcess(path);
    }

    /// <summary>
    /// 使用Notepad2编辑文本文件
    /// </summary>
    /// <param name="args"></param>
    public static void Notepad2EditTextFile(string args)
    {
        OpenProcess(FileHelper.File_Cache_Notepad2, args);
    }

    /// <summary>
    /// 根据进程名字关闭指定程序
    /// </summary>
    /// <param name="processName">程序名字，不需要加.exe</param>
    public static void CloseProcess(string processName)
    {
        var pArray = Process.GetProcessesByName(processName);
        foreach (var process in pArray)
            process.Kill();
    }

    /// <summary>
    /// 关闭全部第三方exe进程
    /// </summary> 
    public static void CloseThirdProcess()
    {
        CloseProcess("Kiddion");
        CloseProcess("GTAHax");
        CloseProcess("BincoHax");
        CloseProcess("LSCHax");
        CloseProcess("Notepad2");
        CloseProcess("Xenos64");
    }
}
