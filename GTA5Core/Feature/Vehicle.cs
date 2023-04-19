using GTA5Core.Native;

namespace GTA5Core.Feature;

public static class Vehicle
{
    /// <summary>
    /// 玩家是否在载具中
    /// </summary>
    public static bool IsInVehicle()
    {
        long pCPed = Globals.GetCPed();
        return Memory.Read<byte>(pCPed + Offsets.CPed_InVehicle) == 0x01;
    }

    /// <summary>
    /// 载具无敌模式
    /// </summary>
    public static void GodMode(bool isEnable)
    {
        if (Globals.GetCVehicle(out long pCVehicle))
            Memory.Write(pCVehicle + Offsets.CPed_CVehicle_God, (byte)(isEnable ? 0x01 : 0x00));
    }

    /// <summary>
    /// 载具安全带
    /// </summary>
    public static void Seatbelt(bool isEnable)
    {
        long pCPed = Globals.GetCPed();
        Memory.Write(pCPed + Offsets.CPed_Seatbelt, (byte)(isEnable ? 0xC9 : 0xC8));
    }

    /// <summary>
    /// 载具隐形
    /// </summary>
    public static void Invisible(bool isEnable)
    {
        if (Globals.GetCVehicle(out long pCVehicle))
            Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Invisible, (byte)(isEnable ? 0x01 : 0x27));
    }

    /// <summary>
    /// 载具附加功能
    /// </summary>
    public static void Extras(short flag)
    {
        if (Globals.GetCVehicle(out long pCVehicle))
        {
            long pCModelInfo = Memory.Read<long>(pCVehicle + Offsets.CPed_CVehicle_CModelInfo);
            Memory.Write(pCModelInfo + Offsets.CPed_CVehicle_CModelInfo_Extras, flag);
        }
    }

    /// <summary>
    /// 载具降落伞，关0，开1
    /// </summary>
    public static void Parachute(bool isEnable)
    {
        if (Globals.GetCVehicle(out long pCVehicle))
        {
            long pCModelInfo = Memory.Read<long>(pCVehicle + Offsets.CPed_CVehicle_CModelInfo);
            Memory.Write(pCModelInfo + Offsets.CPed_CVehicle_CModelInfo_Parachute, (byte)(isEnable ? 0x01 : 0x00));
        }
    }

    /// <summary>
    /// 补满载具血量
    /// </summary>
    public static void FillHealth()
    {
        if (Globals.GetCVehicle(out long pCVehicle))
        {
            float oVHealth = Memory.Read<float>(pCVehicle + Offsets.CPed_CVehicle_Health);
            float oVHealthMax = Memory.Read<float>(pCVehicle + Offsets.CPed_CVehicle_HealthMax);
            if (oVHealth <= oVHealthMax)
            {
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, oVHealthMax);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthBody, oVHealthMax);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthPetrolTank, oVHealthMax);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthEngine, oVHealthMax);
            }
            else
            {
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, 1000.0f);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthBody, 1000.0f);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthPetrolTank, 1000.0f);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthEngine, 1000.0f);
            }
        }
    }

    /// <summary>
    /// 修复载具外观
    /// </summary>
    public static async Task FixVehicleByBST()
    {
        await Task.Run(async () =>
        {
            if (Globals.GetCVehicle(out long pCVehicle))
            {
                Hacks.WriteGA(Offsets.oVMYCar + 894, 1);

                await Task.Delay(1000);

                long pCPickupData = Memory.Read<long>(Pointers.PickupDataPTR);
                long FixVehValue = Memory.Read<long>(pCPickupData + 0x230);       // pFixVeh
                long BSTValue = Memory.Read<long>(pCPickupData + 0x160);          // pBST

                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, 999.0f);

                long pCPickupInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);
                long pCReplayInterface_CPickupInterface = Memory.Read<long>(pCPickupInterface + Offsets.CReplayInterface_CPickupInterface);

                long mPickupCount = Memory.Read<int>(pCReplayInterface_CPickupInterface + 0x110);       // oPickupNum
                long pPickupList = Memory.Read<long>(pCReplayInterface_CPickupInterface + 0x100);       // pPickupList

                await Task.Delay(100);

                for (long i = 0; i < mPickupCount; i++)
                {
                    long dwpPickup = Memory.Read<long>(pPickupList + i * 0x10);
                    if (!Memory.IsValid(dwpPickup))
                        continue;

                    long dwPickupValue = Memory.Read<long>(dwpPickup + 0x470);        // pDroppedPickupData
                    if (!Memory.IsValid(dwPickupValue))
                        continue;

                    if (dwPickupValue == BSTValue || dwPickupValue == FixVehValue)
                    {
                        Memory.Write(dwpPickup + 0x470, FixVehValue);

                        await Task.Delay(10);

                        Vector3 dwpPickupV3 = Memory.Read<Vector3>(dwpPickup + 0x90);
                        Vector3 vehicleV3 = Memory.Read<Vector3>(pCVehicle + Offsets.CPed_CVehicle_VisualX);

                        await Task.Delay(10);

                        Memory.Write(dwpPickup + 0x90, vehicleV3);      // oVPositionX
                        Memory.Write(pCVehicle + 0x9D8, 0.0f);          // oVDirt  float  Wash Vehicle
                    }
                }

                await Task.Delay(1000);

                if (Hacks.ReadGA<int>(Offsets.oNETTimeHelp + 3690) != 0)
                    Hacks.WriteGA(Offsets.oNETTimeHelp + 3690, -1);
            }
        });
    }

    /// <summary>
    /// 请求个人载具
    /// </summary>
    /// <param name="index"></param>
    public static void RequestPersonalVehicle(int index)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 986, index);
        Hacks.WriteGA(Offsets.oVMYCar + 983, 1);
    }
}
