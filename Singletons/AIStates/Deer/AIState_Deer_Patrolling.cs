using System;
using System.Linq;
using Godot;

public class AIState_Deer_Patrolling : State<Cognition> {
	private static readonly Lazy<AIState_Deer_Patrolling> lazy = new(() => new AIState_Deer_Patrolling());
	public static AIState_Deer_Patrolling Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Patrol");

		var wander = new Wander();
		wander.Weight = 0.5f;
		entity.Steering.Behaviours.Add(wander);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var player = entity.Perception.VisibleBodies.Find(x => x.IsInGroup("Player"));
		if (player != null) {
			return AIState_Deer_Watching.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Patrol");

		var wander = entity.Steering.Behaviours.OfType<Wander>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(wander);
	}
}
