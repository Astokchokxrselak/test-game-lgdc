using Godot;
using Godot.Collections;
using System;

public abstract class InteractionResult
{

}
public class OverworldIR : InteractionResult
{
	public Vector2? newPosition;
}
public class DupleCombatIR : InteractionResult
{
	public int? battleID;
	public OverworldIR winResult, lossResult;
}
public class CutsceneIR : InteractionResult
{
	public int? cutsceneID;
}
public abstract partial class NPCData : EntityData, ILowPriorityEntity, ICombatNPC
{
	[Export]
	public float DetectionRadius = -1;  // if -1, does not detect player
	public bool DetectsPlayer => DetectionRadius != -1;
	public virtual bool InteractOnContact { get; }
	public int Health { get; set; }
	public int MaxHealth { get; set; }

	// private Line2D _testline;
	public override void Initialize()
	{
		/*
		_testline = new Line2D();
		AddChild(_testline);
		_testline.GlobalPosition = Vector2.Zero;
		_testline.Points = [default, default];
		*/

		base.Initialize();
		_excludedBodies ??= [default];
		if (DetectsPlayer)
			Init();
	}
	public override abstract void Init();
	public override abstract void OnUpdate(double delta);

	public virtual void OnDetectPlayer(float playerDistance, CharacterController player) { }
	public virtual void OnLostPlayer(float playerDistance, CharacterController player) { }
	public abstract void OnInteract(CharacterController player);
	public abstract InteractionResult PostInteract(CharacterController player);
	private const float RayCastMargin = 8f;

	public void TryDetectIfNear()
	{
		var playerDistance = (PlayerData.Instance.Character.GlobalPosition - GlobalPosition).LengthSquared();
		if (playerDistance < DetectionRadius * DetectionRadius)
		{
			var space_state = GetWorld2D().DirectSpaceState;
			var start = GlobalPosition;
			var end = (PlayerData.Instance.Character.GlobalPosition - GlobalPosition).Normalized() * (DetectionRadius + RayCastMargin) + GlobalPosition;
			var query = PhysicsRayQueryParameters2D.Create(start, end);

			// _testline.SetPointPosition(0, start);
			// _testline.SetPointPosition(1, end);

			_excludedBodies[0] = Character.GetRid();
			query.Exclude = _excludedBodies;

			var result = space_state.IntersectRay(query);
			if (result == null || result.Count == 0)
			{
				GD.Print("No collision detected.");
				return;
			}
			Node2D collider = (Node2D)result["collider"];
			if (collider != null && collider == PlayerData.Instance.Character)
			{
				OnDetectPlayer(MathF.Sqrt(playerDistance), PlayerData.Instance.Character);
			}
		}
	}


	private static Array<Rid> _excludedBodies;
	public sealed override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (DetectsPlayer)
		{
			TryDetectIfNear();
		}
	}
}
