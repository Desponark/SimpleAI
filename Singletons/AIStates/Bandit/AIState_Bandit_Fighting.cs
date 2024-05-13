using System;
using System.Linq;
using Godot;

public class AIState_Bandit_Fighting : State<Cognition> {
	private static readonly Lazy<AIState_Bandit_Fighting> lazy = new(() => new AIState_Bandit_Fighting());
	public static AIState_Bandit_Fighting Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter fighting");

		var target = entity.FocusTarget;

		var flee = new Flee(target, 4);
		entity.Steering.Behaviours.Add(flee);

		var approach = new Approach(target, 6);
		entity.Steering.Behaviours.Add(approach);

		var orbit = new Orbit(target, Random.Shared.NextDouble() >= 0.5);
		orbit.Weight = 0.6f;
		entity.Steering.Behaviours.Add(orbit);

		if (!entity.Memory.ContainsKey("attackInterval"))
			entity.Memory["attackInterval"] = (float)Random.Shared.Next(0, 4);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;

		if (target == null)
			return AIState_Bandit_Patrolling.Instance;

		if (entity.Vehicle.Position.DistanceTo(target.Position) > 15)
			return AIState_Bandit_Chasing.Instance;

		if (entity.Vehicle.Position.DistanceTo(target.Position) < 6) {
			if ((float)entity.Memory["attackInterval"] < 0) {
				entity.Memory["attackInterval"] = (float)Random.Shared.Next(2, 4);
				return AIState_Bandit_Attacking.Instance;
			}
			entity.Memory["attackInterval"] = (float)entity.Memory["attackInterval"] - (float)delta;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + "Exit fighting");

		var flee = entity.Steering.Behaviours.OfType<Flee>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(flee);

		var approach = entity.Steering.Behaviours.OfType<Approach>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(approach);

		var orbit = entity.Steering.Behaviours.OfType<Orbit>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(orbit);
	}
}