using Godot;
using System;
using System.Collections.Generic;

public enum OverworldAIType
{
    Static,
    RandomWalk,
    Player
}
public abstract class OverworldAI
{
    public abstract void UpdateAI(CharacterController character, double delta);
}
public class StaticAI : OverworldAI
{
    public override void UpdateAI(CharacterController character, double delta)
    {
        // Do nothing, character remains static
    }
}
public class RandomWalkAI : OverworldAI
{
    public override void UpdateAI(CharacterController character, double delta)
    {
        // Implement random walk logic
    }
}
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
public static class OverworldAIController
{
    public static bool OverrideAnimation = false, OverrideMovement = false, OverrideCamera = false;
    public static Dictionary<OverworldAIType, OverworldAI> AIDictionary = new Dictionary<OverworldAIType, OverworldAI>();
    public static void Initialize()
    {
        AIDictionary[OverworldAIType.Static] = new StaticAI();
        AIDictionary[OverworldAIType.RandomWalk] = new RandomWalkAI();
        AIDictionary[OverworldAIType.Player] = new PlayerAI();
    }
    public static OverworldAI GetAI(OverworldAIType type)
    {
        return AIDictionary[type];
    }
    public static void UpdateCharacterAI(CharacterController character, OverworldAIType type, double delta)
    {
        OverworldAI ai = GetAI(type);
        ai.UpdateAI(character, delta);
    }
}
