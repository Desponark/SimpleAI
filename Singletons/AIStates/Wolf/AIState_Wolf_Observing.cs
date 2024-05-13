using System;
using Godot;

public class AIState_Wolf_Observing : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Observing> lazy = new(() => new AIState_Wolf_Observing());
	public static AIState_Wolf_Observing Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Observing");
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;

		if (target == null) {
			return AIState_Wolf_Patrolling.Instance;
		}

		entity.Vehicle.LookAt(target.Position);

		var distanceTo = entity.Vehicle.Position.DistanceTo(target.Position);
		entity.GameplayStats.Fight += (float)delta * Mathf.Pow(50 / distanceTo, 2);

		if (entity.GameplayStats.Fight >= entity.GameplayStats.MaxFight) {
			return AIState_Wolf_Chasing.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Observing");
	}
}