using System;
using System.Linq;
using Godot;

public class AIState_Bandit_Chasing : State<Cognition> {
	private static readonly Lazy<AIState_Bandit_Chasing> lazy = new(() => new AIState_Bandit_Chasing());
	public static AIState_Bandit_Chasing Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Chasing");

		var target = entity.FocusTarget;

		var pursuit = new Pursuit(target);
		entity.Steering.Behaviours.Add(pursuit);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;

		if (target == null) {
			return AIState_Bandit_Patrolling.Instance;
		}
		else {
			var distance = entity.Vehicle.Position.DistanceTo(target.Position);
			if (distance < 5) {
				return AIState_Bandit_Fighting.Instance;
			}
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Chasing");

		var pursuit = entity.Steering.Behaviours.OfType<Pursuit>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(pursuit);
	}
}