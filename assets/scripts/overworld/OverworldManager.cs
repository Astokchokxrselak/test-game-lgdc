using Godot;
using System;

// The OverworldManager is responsible for managing the overworld-level logic and initialization.
public static class OverworldManager
{
	public static void Initialize()
	{
		OverworldAIController.Initialize();
	}
}
