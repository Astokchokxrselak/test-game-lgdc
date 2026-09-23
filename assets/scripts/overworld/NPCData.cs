using Godot;
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
public abstract partial class NPCData : EntityData, IEntity, ICombatNPC
{
	public int Health { get; set; }
	public int MaxHealth { get; set; }
	public void Initialize()
	{
		Init();
	}
	public override abstract void Init();
	public override abstract void OnUpdate(double delta);
	public virtual void OnDetectPlayer(float playerDistance, CharacterController player) { }
	public virtual void OnLostPlayer(float playerDistance, CharacterController player) { }
	public abstract void OnInteract(CharacterController player);
	public abstract InteractionResult PostInteract(CharacterController player);
}
