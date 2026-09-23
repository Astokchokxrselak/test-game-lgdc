using Godot;
using System;
using System.Collections.Generic;

// Enum representing the different types of AI behavior for overworld characters
public enum OverworldAIType
{
    Static,
    RandomWalk,
    Player
}

// Base class for overworld AI behavior
public abstract class OverworldAI
{
    public abstract void UpdateAI(CharacterController character, double delta);
}

// Static AI behavior - character remains in place
public class StaticAI : OverworldAI
{
    public override void UpdateAI(CharacterController character, double delta)
    {
        // Do nothing, character remains static
    }
}

// Random walk AI behavior - character moves randomly
public class RandomWalkAI : OverworldAI
{
    public override void UpdateAI(CharacterController character, double delta)
    {
        // Implement random walk logic
    }
}

// Player AI behavior - character responds to player input
public class PlayerAI : OverworldAI
{
    private void HandleInput(CharacterController character, double delta)
    {
        Vector2 inputDirection = Vector2.Zero;

        if (Input.IsActionPressed("ui_right"))
            inputDirection.X += 1;
        if (Input.IsActionPressed("ui_left"))
            inputDirection.X -= 1;
        if (Input.IsActionPressed("ui_down"))
            inputDirection.Y += 1;
        if (Input.IsActionPressed("ui_up"))
            inputDirection.Y -= 1;

        if (inputDirection != Vector2.Zero)
        {
            inputDirection = inputDirection.Normalized();
            character.Move(inputDirection, CharacterController.CharacterDefaultSpeed);
        }
    }
    public override void UpdateAI(CharacterController character, double delta)
    {
        HandleInput(character, delta);
    }
}

// Controller for managing overworld AI behavior based on specific function calls
public static class OverworldAIController
{
    public static bool OverrideAnimation = false, OverrideMovement = false; // used to override AI behavior
    public static Dictionary<OverworldAIType, OverworldAI> AIDictionary = new Dictionary<OverworldAIType, OverworldAI>(); // store various AI implementations associated with their types
    public static void Initialize()
    {
        AIDictionary[OverworldAIType.Static] = new StaticAI();
        AIDictionary[OverworldAIType.RandomWalk] = new RandomWalkAI();
        AIDictionary[OverworldAIType.Player] = new PlayerAI();
    }
    // return the AI instance for the specified type
    public static OverworldAI GetAI(OverworldAIType type)
    {
        return AIDictionary[type];
    }
    // AI behavior update function per frame
    public static void UpdateCharacterAI(CharacterController character, OverworldAIType type, double delta)
    {
        OverworldAI ai = GetAI(type);
        ai.UpdateAI(character, delta);
    }
}
