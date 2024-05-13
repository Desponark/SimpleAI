using System;
using System.Linq;
using Godot;

public class AIState_Wolf_Patrolling : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Patrolling> lazy = new(() => new AIState_Wolf_Patrolling());
	public static AIState_Wolf_Patrolling Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Patrolling");

		var wander = new Wander();
		wander.Weight = 0.5f;
		entity.Steering.Behaviours.Add(wander);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;
		if (target != null) {
			return AIState_Wolf_Observing.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Patrolling");

		var wander = entity.Steering.Behaviours.OfType<Wander>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(wander);
	}
}
