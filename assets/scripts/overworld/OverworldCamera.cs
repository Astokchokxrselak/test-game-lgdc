using Godot;
using System;
using System.Collections.Generic;

// Controller for managing the overworld camera behavior
public static class OverworldCameraController
{
    public static bool OverrideCamera = false;  // overrides default camera behavior
    // updates the camera position based on the player character's position
    public static void UpdateCamera(Node2D camera, CharacterController character, double delta)
    {
        if (camera != null && character != null)
        {
            camera.Position = character.Position;
        }
    }
}

// controls the camera in the overworld scene
public partial class OverworldCamera : Node2D, IEntity
{
    private CharacterController playerCharacter;
    // Caches player character reference
    public void Initialize()
    {
        playerCharacter = PlayerData.PlayerCharacter;
    }

    public override void _Process(double delta)
    {
        if (OverworldCameraController.OverrideCamera)
            return;
        OverworldCameraController.UpdateCamera(this, playerCharacter, delta);
    }
}