using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
	[Export]
	public OverworldAIType AIType = OverworldAIType.Static;
	public static float CharacterDefaultSpeed = 100f;  // in px/sec
													   // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public void Move(Vector2 direction, float speed)
	{
		Velocity = direction * speed;
		MoveAndSlide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		OverworldAIController.UpdateCharacterAI(this, AIType, delta);
	}
}
