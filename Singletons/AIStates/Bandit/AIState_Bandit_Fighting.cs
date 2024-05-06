using System;
using System.Diagnostics;
using System.Linq;
using Godot;

public class AIState_Bandit_Fighting : State<Cognition> {
	private static readonly Lazy<AIState_Bandit_Fighting> lazy = new(() => new AIState_Bandit_Fighting());
	public static AIState_Bandit_Fighting Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		Debug.WriteLine("Enter fighting");

		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		var flee = new Flee(player, 4);
		entity.Steering.Behaviours.Add(flee);

		var approach = new Approach(player, 6);
		entity.Steering.Behaviours.Add(approach);

		var orbit = new Orbit(player, Random.Shared.NextDouble() >= 0.5);
		entity.Steering.Behaviours.Add(orbit);

		entity.Memory["attackInterval"] = (float)Random.Shared.Next(2, 6);
	}

	public override void Execute(Cognition entity, double delta) {
		var player = (Vehicle)entity.Memory["lastSeenPlayer"];
		if (player == null) {
			entity.StateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
			return;
		}

		if (entity.Vehicle.Position.DistanceTo(player.Position) > 15) {
			entity.StateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
		}

		if (entity.Vehicle.Position.DistanceTo(player.Position) < 6) {
			if ((float)entity.Memory["attackInterval"] < 0) {
				entity.StateMachine.ChangeState(AIState_Bandit_Attacking.Instance);
			}
			entity.Memory["attackInterval"] = (float)entity.Memory["attackInterval"] - (float)delta;
		}
	}

	public override void Exit(Cognition entity) {
		Debug.WriteLine("Exit fighting");

		var flee = entity.Steering.Behaviours.OfType<Flee>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(flee);

		var approach = entity.Steering.Behaviours.OfType<Approach>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(approach);

		var orbit = entity.Steering.Behaviours.OfType<Orbit>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(orbit);
	}
}