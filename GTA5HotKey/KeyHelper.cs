using System;
using System.Runtime.InteropServices;

namespace GTA5HotKey;

public static class KeyHelper
{
    [DllImport("user32.dll")]
    public static extern int GetAsyncKeyState(int vKey);

    [DllImport("user32.dll", EntryPoint = "keybd_event")]
    public static extern void Keybd_Event(WinVK bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern uint MapVirtualKey(WinVK uCode, uint uMapType);

    public const int KEY_PRESSED = 0x8000;

    /// <summary>
    /// 判断按键是否按下
    /// </summary>
    /// <param name="nVirtKey"></param>
    /// <returns></returns>
    public static bool IsKeyPressed(WinVK nVirtKey)
    {
        return Convert.ToBoolean(GetAsyncKeyState((int)nVirtKey) & KEY_PRESSED);
    }
}
