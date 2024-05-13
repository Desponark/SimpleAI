using System;
using Godot;

public class AIState_Deer_Watching : State<Cognition> {
	private static readonly Lazy<AIState_Deer_Watching> lazy = new(() => new AIState_Deer_Watching());
	public static AIState_Deer_Watching Instance { get { return lazy.Value; } }

	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Watching");
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;
		if (target == null) {
			return AIState_Deer_Patrolling.Instance;
		}

		entity.Vehicle.LookAt(target.Position);

		var distanceTo = entity.Vehicle.Position.DistanceTo(target.Position);
		entity.GameplayStats.Flight += (float)delta * Mathf.Pow(50 / distanceTo, 2);

		if (entity.GameplayStats.Flight >= entity.GameplayStats.MaxFlight) {
			return AIState_Deer_Fleeing.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Watching");
	}
}