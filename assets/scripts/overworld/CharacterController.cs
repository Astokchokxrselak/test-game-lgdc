using Godot;
using System;

public partial class CharacterController : CharacterBody2D, IEntity
{
	[Export]
	public OverworldAIType AIType = OverworldAIType.Static;
	public static float CharacterDefaultSpeed = 100f;  // in px/sec
													   // Called when the node enters the scene tree for the first time.

	// Do not use Ready(), use Initialize() instead for initialization to ensure proper order of initialization.
	public void Initialize()
	{
		animationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
	}
	private string currentAnimation;
	private AnimationPlayer animationPlayer;
	public void PlayAnimation(string animationName)
	{
		if (animationPlayer != null && animationPlayer.HasAnimation(animationName))
		{
			currentAnimation = animationName;
		}
		else
		{
			GD.PrintErr($"Animation '{animationName}' not found in AnimationPlayer.");
		}
	}

	private void UpdateAnimation()
	{
		if (animationPlayer != null && !string.IsNullOrEmpty(currentAnimation))
		{
			if (!animationPlayer.IsPlaying() || animationPlayer.CurrentAnimation != currentAnimation)
			{
				animationPlayer.Play(currentAnimation);
			}
		}
	}

	public void Move(Vector2 direction, float speed)
	{
		Velocity = direction * speed;
		PlayAnimation("moving");
		MoveAndSlide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		PlayAnimation("idle");
		OverworldAIController.UpdateCharacterAI(this, AIType, delta);
		UpdateAnimation();
	}
}
