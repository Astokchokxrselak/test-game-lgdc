using Godot;
using System;

// this script stores player data for global use and caching
public partial class PlayerData : EntityData, ICombatNPC
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public static PlayerData Instance { get; private set; }
    public override void Init()
    {
        Instance = this;
        // Initialization logic for player-specific node data
    }
    public override void OnUpdate(double delta)
    {
        // Update logic for player-specific node data
    }
}