using System;
using System.Diagnostics;
using System.Linq;
using Godot;

public class AIState_Bandit_Patrolling : State<Cognition> {
	private static readonly Lazy<AIState_Bandit_Patrolling> lazy = new(() => new AIState_Bandit_Patrolling());
	public static AIState_Bandit_Patrolling Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Patrol");

		var wander = new Wander();
		entity.Steering.Behaviours.Add(wander);
	}

	public override void Execute(Cognition entity, double delta) {
		var player = entity.Perception.VisibleBodies.Find(x => x.IsInGroup("Player"));
		if (player != null) {
			entity.StateMachine.ChangeState(AIState_Bandit_Chasing.Instance);
		}
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Patrol");

		var wander = entity.Steering.Behaviours.OfType<Wander>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(wander);
	}
}
