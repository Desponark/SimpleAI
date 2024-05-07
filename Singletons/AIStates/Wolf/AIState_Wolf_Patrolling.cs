using System;
using System.Linq;
using Godot;

public class AIState_Wolf_Patrolling : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Patrolling> lazy = new(() => new AIState_Wolf_Patrolling());
	public static AIState_Wolf_Patrolling Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity + " Enter Patrolling");

		var wander = new Wander();
		wander.Weight = 0.5f;
		entity.Steering.Behaviours.Add(wander);
	}

	public override void Execute(Cognition entity, double delta) {
		var player = (Vehicle)entity.Memory["lastSeenPlayer"];
		if (player != null) {
			entity.StateMachine.ChangeState(AIState_Wolf_Observing.Instance);
		}
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity + " Enter Patrolling");

		var wander = entity.Steering.Behaviours.OfType<Wander>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(wander);
	}
}
