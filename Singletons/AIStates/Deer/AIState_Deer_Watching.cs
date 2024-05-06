using System;
using Godot;

public class AIState_Deer_Watching : State<Cognition> {
	private static readonly Lazy<AIState_Deer_Watching> lazy = new(() => new AIState_Deer_Watching());
	public static AIState_Deer_Watching Instance { get { return lazy.Value; } }

	public override void Enter(Cognition entity) {
		GD.Print("Enter Watching");
	}

	public override void Execute(Cognition entity, double delta) {
		var player = (Vehicle)entity.Memory["lastSeenPlayer"];
		if (player == null) {
			entity.StateMachine.ChangeState(AIState_Deer_Patrolling.Instance);
			return;
		}

		entity.Vehicle.LookAt(player.Position);

		var distanceTo = entity.Vehicle.Position.DistanceTo(player.Position);
		if (distanceTo <= 18) {
			entity.GameplayStats.Flight += (float)delta * (300 / distanceTo);
		}

		if (entity.GameplayStats.Flight >= entity.GameplayStats.MaxFlight) {
			entity.StateMachine.ChangeState(AIState_Deer_Fleeing.Instance);
		}
	}

	public override void Exit(Cognition entity) {
		GD.Print("Exit Watching");
	}
}