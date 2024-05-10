using System;
using System.Linq;
using Godot;

public class AIState_Bandit_Chasing : State<Cognition> {
	private static readonly Lazy<AIState_Bandit_Chasing> lazy = new(() => new AIState_Bandit_Chasing());
	public static AIState_Bandit_Chasing Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Chasing");

		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		var pursuit = new Pursuit(player);
		entity.Steering.Behaviours.Add(pursuit);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var player = (Node3D)entity.Memory["lastSeenPlayer"];

		if (player == null) {
			return AIState_Bandit_Patrolling.Instance;
		}
		else {
			var distance = entity.Vehicle.Position.DistanceTo(player.Position);
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