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
public abstract partial class NPCData : Node2D, IEntity
{
	public int Health { get; set; }
	public int MaxHealth { get; set; }
	private CharacterController Character;
	public void Initialize()
	{
		Character = GetParent<CharacterController>();  // this object (the PlayerData) is attached to the player character
		Init();
	}
	public abstract void Init();
	public virtual void OnUpdate(double delta) { }
	public virtual void OnDetectPlayer(float playerDistance, CharacterController player) { }
	public virtual void OnLostPlayer(float playerDistance, CharacterController player) { }
	public abstract void OnInteract(CharacterController player);
	public abstract InteractionResult PostInteract(CharacterController player);
}
