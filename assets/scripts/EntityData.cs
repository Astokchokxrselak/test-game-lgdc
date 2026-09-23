using Godot;
using System;

public interface ICombatNPC
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
}

// this script stores player data for global use and caching
public abstract partial class EntityData : Node2D, IEntity
{
    private CharacterController character;
    public CharacterController Character { get => character; }
    public void Initialize()
    {
        character = GetParent<CharacterController>();  // this object (the PlayerData) is attached to the player character
                                                       // Initialization logic for player-specific node data
        Init();
    }
    public abstract void Init();
    public abstract void OnUpdate(double delta);
    public override void _PhysicsProcess(double delta)
    {
        OnUpdate(delta);
    }
}