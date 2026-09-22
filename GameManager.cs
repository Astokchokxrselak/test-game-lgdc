using Godot;
using System;

public partial class GameManager : Node2D
{
	public static GameManager Singleton { get; private set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Singleton = this;
		NodeManager.Initialize();
		OverworldManager.Initialize();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
