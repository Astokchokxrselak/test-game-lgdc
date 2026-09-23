using Godot;
using System;

// this script stores player data for global use and caching
public partial class PlayerData : Node2D, IEntity
{
    public static CharacterController PlayerCharacter { get; private set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public void Initialize()
    {
        PlayerCharacter = GetParent<CharacterController>();  // this object (the PlayerData) is attached to the player character
    }
}