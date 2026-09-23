using Godot;
using System;

// Character controller for handling character movement and animation in the overworld.
public partial class CharacterController : CharacterBody2D, IEntity
{
	[Export]
	public OverworldAIType AIType = OverworldAIType.Static;  // The type of AI behavior for this character
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
	public void UpdateAnimation(string name)
	{
		if (!OverworldAIController.OverrideAnimation)
		{
			throw new ArgumentException("UpdateAnimation(string name) should only be called when OverrideAnimation is true.");
		}
		if (animationPlayer != null && !string.IsNullOrEmpty(currentAnimation))
		{
			if (!animationPlayer.IsPlaying() || animationPlayer.CurrentAnimation != currentAnimation)
			{
				animationPlayer.Play(name);
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
	public void MovementProcess(double delta)
	{
		PlayAnimation("idle");
		if (OverworldAIController.OverrideMovement)
		{
			return;
		}
		OverworldAIController.UpdateCharacterAI(this, AIType, delta);
	}
	public void AnimationProcess(double delta)
	{
		if (OverworldAIController.OverrideAnimation)
		{
			return;
		}
		UpdateAnimation();
	}
	public override void _PhysicsProcess(double delta)
	{
		AnimationProcess(delta);
		MovementProcess(delta);
	}
}
