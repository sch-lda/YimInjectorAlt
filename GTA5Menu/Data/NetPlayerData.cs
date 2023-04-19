﻿namespace GTA5Menu.Data;

public class NetPlayerData
{
    public int Rank { get; set; }
    public long RockstarId { get; set; }
    public string PlayerName { get; set; }

    public long Money { get; set; }
    public long Bank { get; set; }
    public long Cash { get; set; }

    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Armor { get; set; }
    public bool GodMode { get; set; }
    public bool NoRagdoll { get; set; }

    public byte WantedLevel { get; set; }
    public float WalkSpeed { get; set; }
    public float RunSpeed { get; set; }
    public float SwimSpeed { get; set; }

    public float Distance { get; set; }
    public Vector3 Position { get; set; }

    public string ClanName { get; set; }
    public string ClanTag { get; set; }
}
