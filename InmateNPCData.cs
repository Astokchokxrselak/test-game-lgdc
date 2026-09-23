using Godot;
using System;

public partial class InmateNPCData : NPCData
{
	public override InteractionResult PostInteract(CharacterController player)
	{
		throw new NotImplementedException();
	}
	public override void OnInteract(CharacterController player)
	{
		throw new NotImplementedException();
	}
	public override void Init()
	{
		throw new NotImplementedException();
	}
	public override void OnUpdate(double delta)
	{
		throw new NotImplementedException();
	}
	public override void OnDetectPlayer(float playerDistance, CharacterController player)
	{
		GD.Print("👁️");
	}
}
