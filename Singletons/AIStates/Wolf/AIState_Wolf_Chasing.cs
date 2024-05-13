using System;
using System.Linq;
using Godot;

public class AIState_Wolf_Chasing : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Chasing> lazy = new(() => new AIState_Wolf_Chasing());
	public static AIState_Wolf_Chasing Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Chasing");

		var target = entity.FocusTarget;

		var pursuit = new Pursuit(target);
		entity.Steering.Behaviours.Add(pursuit);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;

		if (target == null) {
			return AIState_Wolf_Observing.Instance;
		}

		var distance = entity.Vehicle.Position.DistanceTo(target.Position);
		if (distance < 6) {
			return AIState_Wolf_Attacking.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Chasing");

		var pursuit = entity.Steering.Behaviours.OfType<Pursuit>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(pursuit);
	}
}