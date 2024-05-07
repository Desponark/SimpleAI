using System;
using System.Linq;
using Godot;

public class AIState_Wolf_Attacking : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Attacking> lazy = new(() => new AIState_Wolf_Attacking());
	public static AIState_Wolf_Attacking Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity + " Enter Attacking");

		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		var seek = new Seek(player);
		seek.Weight = 2;
		entity.Steering.Behaviours.Add(seek);

		entity.Memory["attackDuration"] = 2f;
	}

	public override void Execute(Cognition entity, double delta) {
		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		if (player == null) {
			entity.StateMachine.ChangeState(AIState_Wolf_Observing.Instance);
			return;
		}
		if ((float)entity.Memory["attackDuration"] < 0f) {
			entity.StateMachine.ChangeState(AIState_Wolf_Disengaging.Instance);
		}
		entity.Memory["attackDuration"] = (float)entity.Memory["attackDuration"] - (float)delta;

		if (entity.Vehicle.Position.DistanceTo(player.Position) <= 1.5) {
			GD.Print(entity + " ATTACKED " + player);
			entity.GameplayStats.Fight -= 33;
			entity.StateMachine.ChangeState(AIState_Wolf_Disengaging.Instance);
		}
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity + " Exit Attacking");

		var seek = entity.Steering.Behaviours.OfType<Seek>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(seek);
	}
}