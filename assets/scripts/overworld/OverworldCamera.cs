using Godot;
using System;
using System.Collections.Generic;

public static class OverworldCameraController
{
    public static void UpdateCamera(Camera2D camera, CharacterController character, double delta)
    {
        if (OverworldAIController.OverrideCamera)
            return;

        if (camera != null && character != null)
        {
            camera.Position = character.Position;
        }
    }
}

public partial class OverworldCamera : Camera2D, IEntity
{
    private CharacterController playerCharacter;

    public void Initialize()
    {
        playerCharacter = PlayerData.PlayerCharacter;
    }

    public override void _Process(double delta)
    {
        OverworldCameraController.UpdateCamera(this, playerCharacter, delta);
    }
}