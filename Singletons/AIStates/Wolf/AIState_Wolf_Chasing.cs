using System;
using System.Linq;
using Godot;

public class AIState_Wolf_Chasing : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Chasing> lazy = new(() => new AIState_Wolf_Chasing());
	public static AIState_Wolf_Chasing Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Chasing");

		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		var pursuit = new Pursuit(player);
		entity.Steering.Behaviours.Add(pursuit);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var player = (Node3D)entity.Memory["lastSeenPlayer"];

		if (player == null) {
			return AIState_Wolf_Observing.Instance;
		}

		var distance = entity.Vehicle.Position.DistanceTo(player.Position);
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