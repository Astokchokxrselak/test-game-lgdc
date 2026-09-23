using Godot;
using System;

// this script stores player data for global use and caching
public partial class PlayerData : EntityData, ICombatNPC, IHighPriorityEntity
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public static PlayerData Instance { get; private set; }
    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
        // Initialization logic for player-specific node data
    }
    public override void Init()
    {
        // Initialization logic for player-specific node data
    }
    public override void OnUpdate(double delta)
    {
        // Update logic for player-specific node data
    }
}