﻿using GTA5Core.Feature;

namespace GTA5Core.Native;

public static class Memory
{
    /// <summary>
    /// 是否初始化成功
    /// </summary>
    public static bool IsInitialized { get; set; }

    /// <summary>
    /// GTA5进程类
    /// </summary>
    public static Process GTA5Process { get; set; } = null;
    /// <summary>
    /// GTA5窗口句柄
    /// </summary>
    public static IntPtr GTA5WinHandle { get; set; } = IntPtr.Zero;
    /// <summary>
    /// GTA5进程Id
    /// </summary>
    public static int GTA5ProId { get; set; } = 0;
    /// <summary>
    /// GTA5主模块基址
    /// </summary>
    public static long GTA5ProBaseAddress { get; set; } = 0;
    /// <summary>
    /// GTA5进程句柄
    /// </summary>
    public static IntPtr GTA5ProHandle { get; set; } = IntPtr.Zero;

    /// <summary>
    /// 关闭GTA5进程句柄并清理资源
    /// </summary>
    public static void CloseHandle()
    {
        if (GTA5ProHandle != IntPtr.Zero)
        {
            IsInitialized = false;

            Win32.CloseHandle(GTA5ProHandle);

            GTA5Process = null;
            GTA5WinHandle = IntPtr.Zero;
            GTA5ProId = 0;
            GTA5ProBaseAddress = 0;
            GTA5ProHandle = IntPtr.Zero;

            Pointers.WorldPTR = 0;
            Pointers.BlipPTR = 0;
            Pointers.GlobalPTR = 0;
            Pointers.NetworkPlayerMgrPTR = 0;
            Pointers.ReplayInterfacePTR = 0;
            Pointers.NetworkInfoPTR = 0;
            Pointers.ViewPortPTR = 0;
            Pointers.CCameraPTR = 0;
            Pointers.AimingPedPTR = 0;
            Pointers.WeatherPTR = 0;
            Pointers.PickupDataPTR = 0;
            Pointers.UnkModelPTR = 0;
            Pointers.LocalScriptsPTR = 0;
            Pointers.UnkPTR = 0;
        }
    }

    /// <summary>
    /// 判断GTA5窗口是否在最前
    /// </summary>
    public static bool IsForegroundWindow()
    {
        return GTA5WinHandle == Win32.GetForegroundWindow();
    }

    /// <summary>
    /// 将GTA5窗口置于最前方
    /// </summary>
    public static void SetForegroundWindow()
    {
        Win32.SetForegroundWindow(GTA5WinHandle);
    }

    /// <summary>
    /// 是否置顶GTA5窗口
    /// </summary>
    public static void TopMostProcess(bool isTopMost)
    {
        if (isTopMost)
            Win32.SetWindowPos(GTA5WinHandle, -1, 0, 0, 0, 0, 1 | 2);
        else
            Win32.SetWindowPos(GTA5WinHandle, -2, 0, 0, 0, 0, 1 | 2);
    }

    /// <summary>
    /// 获取GTA5窗口数据信息
    /// </summary>
    /// <returns></returns>
    public static WindowData GetGameWindowData()
    {
        // 获取指定窗口句柄的窗口矩形数据和客户区矩形数据
        Win32.GetWindowRect(GTA5WinHandle, out W32RECT windowRect);
        Win32.GetClientRect(GTA5WinHandle, out W32RECT clientRect);

        // 计算窗口区的宽和高
        var windowWidth = windowRect.Right - windowRect.Left;
        var windowHeight = windowRect.Bottom - windowRect.Top;

        // 处理窗口最小化
        if (windowRect.Left == -32000)
        {
            return new WindowData()
            {
                Left = 0,
                Top = 0,
                Width = 1,
                Height = 1
            };
        }

        // 计算客户区的宽和高
        var clientWidth = clientRect.Right - clientRect.Left;
        var clientHeight = clientRect.Bottom - clientRect.Top;

        // 计算边框
        var borderWidth = (windowWidth - clientWidth) / 2;
        var borderHeight = windowHeight - clientHeight - borderWidth;

        return new WindowData()
        {
            Left = windowRect.Left += borderWidth,
            Top = windowRect.Top += borderHeight,
            Width = clientWidth,
            Height = clientHeight
        };
    }

    /// <summary>
    /// 特征码搜索，结果为单个地址
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static long FindPattern(string pattern)
    {
        long address = 0;

        var tempArray = new List<byte>();
        foreach (var each in pattern.Split(' '))
        {
            if (each == "??")
            {
                tempArray.Add(Convert.ToByte("0", 16));
            }
            else
            {
                tempArray.Add(Convert.ToByte(each, 16));
            }
        }

        var patternByteArray = tempArray.ToArray();

        var moduleSize = GTA5Process.MainModule.ModuleMemorySize;
        if (moduleSize == 0)
            throw new Exception($"模块 {GTA5Process.MainModule.ModuleName} 大小无效");

        var localModulebytes = new byte[moduleSize];
        Win32.ReadProcessMemory(GTA5ProHandle, GTA5ProBaseAddress, localModulebytes, moduleSize, out _);

        for (int indexAfterBase = 0; indexAfterBase < localModulebytes.Length; indexAfterBase++)
        {
            bool noMatch = false;

            if (localModulebytes[indexAfterBase] != patternByteArray[0])
                continue;

            for (var MatchedIndex = 0; MatchedIndex < patternByteArray.Length && indexAfterBase + MatchedIndex < localModulebytes.Length; MatchedIndex++)
            {
                if (patternByteArray[MatchedIndex] == 0x0)
                    continue;
                if (patternByteArray[MatchedIndex] != localModulebytes[indexAfterBase + MatchedIndex])
                {
                    noMatch = true;
                    break;
                }
            }

            if (!noMatch)
                return GTA5ProBaseAddress + indexAfterBase;
        }

        return address;
    }

    /// <summary>
    /// 特征码搜索，结果为多个地址
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<long> FindPatterns(string pattern)
    {
        List<long> address = new();

        var patternByteArray = Encoding.ASCII.GetBytes(pattern);

        var moduleSize = GTA5Process.MainModule.ModuleMemorySize;
        if (moduleSize == 0)
            throw new Exception($"模块 {GTA5Process.MainModule.ModuleName} 大小无效");

        var localModulebytes = new byte[moduleSize];
        Win32.ReadProcessMemory(GTA5ProHandle, GTA5ProBaseAddress, localModulebytes, moduleSize, out _);

        for (int indexAfterBase = 0; indexAfterBase < localModulebytes.Length; indexAfterBase++)
        {
            bool noMatch = false;

            if (localModulebytes[indexAfterBase] != patternByteArray[0])
                continue;

            for (var MatchedIndex = 0; MatchedIndex < patternByteArray.Length && indexAfterBase + MatchedIndex < localModulebytes.Length; MatchedIndex++)
            {
                if (patternByteArray[MatchedIndex] == 0x0)
                    continue;
                if (patternByteArray[MatchedIndex] != localModulebytes[indexAfterBase + MatchedIndex])
                {
                    noMatch = true;
                    break;
                }
            }

            if (!noMatch)
                address.Add(GTA5ProBaseAddress + indexAfterBase);
        }

        return address;
    }

    public static long Rip_37(long address)
    {
        return address + Read<int>(address + 0x03) + 0x07;
    }

    public static long Rip_6A(long address)
    {
        return address + Read<int>(address + 0x06) + 0x0A;
    }

    /// <summary>
    /// 获取取偏移数组指针
    /// </summary>
    /// <param name="pointer"></param>
    /// <param name="offset"></param>
    private static long GetPtrAddress(long pointer, int[] offset)
    {
        var buffer = new byte[8];
        Win32.ReadProcessMemory(GTA5ProHandle, pointer, buffer, buffer.Length, out _);

        for (int i = 0; i < (offset.Length - 1); i++)
        {
            pointer = BitConverter.ToInt64(buffer, 0) + offset[i];
            Win32.ReadProcessMemory(GTA5ProHandle, pointer, buffer, buffer.Length, out _);
        }

        return BitConverter.ToInt64(buffer, 0) + offset[^1];
    }

    /// <summary>
    /// 泛型读取内存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="address"></param>
    /// <returns></returns>
    public static T Read<T>(long address) where T : struct
    {
        var buffer = new byte[Marshal.SizeOf(typeof(T))];
        Win32.ReadProcessMemory(GTA5ProHandle, address, buffer, buffer.Length, out _);
        return ByteArrayToStructure<T>(buffer);
    }

    /// <summary>
    /// 泛型读取内存，带偏移数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="basePtr"></param>
    /// <param name="offsets"></param>
    public static T Read<T>(long basePtr, int[] offsets) where T : struct
    {
        var buffer = new byte[Marshal.SizeOf(typeof(T))];
        Win32.ReadProcessMemory(GTA5ProHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
        return ByteArrayToStructure<T>(buffer);
    }

    /// <summary>
    /// 泛型写入内存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="basePtr"></param>
    /// <param name="offsets"></param>
    /// <param name="value"></param>
    public static void Write<T>(long address, T value) where T : struct
    {
        var buffer = StructureToByteArray(value);
        Win32.WriteProcessMemory(GTA5ProHandle, address, buffer, buffer.Length, out _);
    }

    /// <summary>
    /// 泛型写入内存，带偏移数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="address"></param>
    /// <param name="value"></param>
    public static void Write<T>(long basePtr, int[] offsets, T value) where T : struct
    {
        var buffer = StructureToByteArray(value);
        Win32.WriteProcessMemory(GTA5ProHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
    }

    /// <summary>
    /// 读取矩阵数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="address"></param>
    /// <param name="MatrixSize"></param>
    /// <returns></returns>
    public static float[] ReadMatrix<T>(long address, int MatrixSize) where T : struct
    {
        var ByteSize = Marshal.SizeOf(typeof(T));
        var buffer = new byte[ByteSize * MatrixSize];
        Win32.ReadProcessMemory(GTA5ProHandle, address, buffer, buffer.Length, out _);
        return ConvertToFloatArray(buffer);
    }

    /// <summary>
    /// 读取字符串
    /// </summary>
    /// <param name="address"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string ReadString(long address, int size)
    {
        var buffer = new byte[size];
        Win32.ReadProcessMemory(GTA5ProHandle, address, buffer, size, out _);

        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] == 0)
            {
                byte[] _buffer = new byte[i];
                Buffer.BlockCopy(buffer, 0, _buffer, 0, i);
                return Encoding.UTF8.GetString(_buffer);
            }
        }

        return Encoding.UTF8.GetString(buffer);
    }

    /// <summary>
    /// 读取字符串，带偏移数组
    /// </summary>
    /// <param name="basePtr"></param>
    /// <param name="offsets"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string ReadString(long basePtr, int[] offsets, int size)
    {
        var buffer = new byte[size];
        Win32.ReadProcessMemory(GTA5ProHandle, GetPtrAddress(basePtr, offsets), buffer, size, out _);

        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] == 0)
            {
                var _buffer = new byte[i];
                Buffer.BlockCopy(buffer, 0, _buffer, 0, i);
                return Encoding.UTF8.GetString(_buffer);
            }
        }

        return Encoding.UTF8.GetString(buffer);
    }

    /// <summary>
    /// 写入字符串
    /// </summary>
    /// <param name="address"></param>
    /// <param name="str"></param>
    public static void WriteString(long address, string str)
    {
        var buffer = new UTF8Encoding().GetBytes(str);
        Win32.WriteProcessMemory(GTA5ProHandle, address, buffer, buffer.Length, out _);
    }

    /// <summary>
    /// 写入字符串，带偏移数组
    /// </summary>
    /// <param name="basePtr"></param>
    /// <param name="offsets"></param>
    /// <param name="str"></param>
    public static void WriteString(long basePtr, int[] offsets, string str)
    {
        var buffer = new UTF8Encoding().GetBytes(str);
        Win32.WriteProcessMemory(GTA5ProHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
    }

    /// <summary>
    /// 读取字节数组
    /// </summary>
    /// <param name="basePtr"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static byte[] ReadBytes(long basePtr, int size)
    {
        var buffer = new byte[size];
        Win32.ReadProcessMemory(GTA5ProHandle, basePtr, buffer, size, out _);
        return buffer;
    }

    /// <summary>
    /// 写入字节数组
    /// </summary>
    /// <param name="basePtr"></param>
    /// <param name="bytes"></param>
    public static void WriteBytes(long basePtr, byte[] bytes)
    {
        Win32.WriteProcessMemory(GTA5ProHandle, basePtr, bytes, bytes.Length, out _);
    }

    //////////////////////////////////////////////////////////////////

    /// <summary>
    /// 判断内存地址是否有效
    /// </summary>
    /// <param name="Address"></param>
    public static bool IsValid(long Address)
    {
        return Address >= 0x10000 && Address < 0x000F000000000000;
    }

    /// <summary>
    /// 字节数组转结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bytes"></param>
    /// <returns></returns>
    private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
    {
        var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        try
        {
            var obj = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            if (obj != null)
                return (T)obj;
            else
                return default;
        }
        finally
        {
            handle.Free();
        }
    }

    /// <summary>
    /// 结构转字节数组
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static byte[] StructureToByteArray(object obj)
    {
        var length = Marshal.SizeOf(obj);
        var array = new byte[length];
        IntPtr pointer = Marshal.AllocHGlobal(length);
        Marshal.StructureToPtr(obj, pointer, true);
        Marshal.Copy(pointer, array, 0, length);
        Marshal.FreeHGlobal(pointer);
        return array;
    }

    /// <summary>
    /// 转换浮点型数组
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static float[] ConvertToFloatArray(byte[] bytes)
    {
        if (bytes.Length % 4 != 0)
        {
            throw new ArgumentException();
        }

        var floats = new float[bytes.Length / 4];
        for (int i = 0; i < floats.Length; i++)
        {
            floats[i] = BitConverter.ToSingle(bytes, i * 4);
        }
        return floats;
    }
}

public struct WindowData
{
    public int Left;
    public int Top;
    public int Width;
    public int Height;
}
