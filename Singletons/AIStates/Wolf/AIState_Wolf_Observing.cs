using System;
using Godot;

public class AIState_Wolf_Observing : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Observing> lazy = new(() => new AIState_Wolf_Observing());
	public static AIState_Wolf_Observing Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity + " Enter Observing");
	}

	public override void Execute(Cognition entity, double delta) {
		var player = (Node3D)entity.Memory["lastSeenPlayer"];

		if (player == null) {
			entity.StateMachine.ChangeState(AIState_Wolf_Patrolling.Instance);
			return;
		}

		entity.Vehicle.LookAt(player.Position);

		var distanceTo = entity.Vehicle.Position.DistanceTo(player.Position);
		if (distanceTo <= 18) {
			entity.GameplayStats.Fight += (float)delta * (300 / distanceTo);
		}

		if (entity.GameplayStats.Fight >= entity.GameplayStats.MaxFight) {
			entity.StateMachine.ChangeState(AIState_Wolf_Chasing.Instance);
		}

	}

	public override void Exit(Cognition entity) {
		GD.Print(entity + " Exit Observing");
	}
}