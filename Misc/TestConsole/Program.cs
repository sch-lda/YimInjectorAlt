using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TestConsole;

internal class Program
{
    static void Main(string[] args)
    {
        //var bytes1 = Encoding.Unicode.GetBytes("F:\\Downloads\\新建文件夹\\YimMenu.dll");
        //Console.WriteLine(bytes1.Length);
        //var bytes2 = Encoding.Unicode.GetBytes("F:\\Downloads\\YimMenu.dll");
        //Console.WriteLine(bytes2.Length);

        Console.WriteLine("操作结束，按任意键继续！");
        Console.ReadLine();
    }

    void Func()
    {
        // 2023/02/07   793
        var vehicles = Enum.GetValues(typeof(VehicleHash));
        Console.WriteLine($"载具数量：{vehicles.Length}");

        var path = "E:\\GTA5\\GTA5图片数据\\Vehicles\\20230205";
        var imgNames = new List<string>();
        foreach (var imgFile in Directory.GetFiles(path))
        {
            imgNames.Add(Path.GetFileNameWithoutExtension(imgFile));
        }

        foreach (var imgName in imgNames)
        {
            //Console.WriteLine(imgName);
        }

        foreach (VehicleHash vEnum in vehicles)
        {
            Console.WriteLine($"{vEnum} 0x{Joaat(vEnum.ToString()):X8}");
            //Console.WriteLine($"{vEnum}");
            //Console.WriteLine($"0x{Joaat(vEnum.ToString()):X8}");

            //Console.WriteLine($"{vEnum.ToString().ToUpper()} = 0x{Joaat(vEnum.ToString()):X8},");

            //var index = imgNames.IndexOf(vEnum.ToString());
            //if (index == -1)
            //{
            //    Console.WriteLine($"{vEnum}");
            //}
            //else
            //{
            //    Console.WriteLine($"{vEnum} {imgNames[index]}");
            //}
        }
    }

    static uint Joaat(string data)
    {
        uint hash = 0u;

        foreach (char c in data.ToLower())
        {
            hash += c;
            hash += hash << 10;
            hash ^= hash >> 6;
        }

        hash += hash << 3;
        hash ^= hash >> 11;
        hash += hash << 15;

        return hash;
    }
}