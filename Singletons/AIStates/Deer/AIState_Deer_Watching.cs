using System;
using Godot;

public class AIState_Deer_Watching : State<Cognition> {
	private static readonly Lazy<AIState_Deer_Watching> lazy = new(() => new AIState_Deer_Watching());
	public static AIState_Deer_Watching Instance { get { return lazy.Value; } }

	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Watching");
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var player = (Vehicle)entity.Memory["lastSeenPlayer"];
		if (player == null) {
			return AIState_Deer_Patrolling.Instance;
		}

		entity.Vehicle.LookAt(player.Position);

		var distanceTo = entity.Vehicle.Position.DistanceTo(player.Position);
		if (distanceTo <= 18) {
			entity.GameplayStats.Flight += (float)delta * (300 / distanceTo);
		}

		if (entity.GameplayStats.Flight >= entity.GameplayStats.MaxFlight) {
			return AIState_Deer_Fleeing.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Watching");
	}
}