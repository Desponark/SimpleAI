using System;
using System.Diagnostics;
using System.Linq;
using Godot;

public class AIState_Bandit_Attacking : State<Cognition> {
	private static readonly Lazy<AIState_Bandit_Attacking> lazy = new(() => new AIState_Bandit_Attacking());
	public static AIState_Bandit_Attacking Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter attacking");

		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		var seek = new Seek(player);
		entity.Steering.Behaviours.Add(seek);

		entity.Memory["attackDuration"] = 2f;
	}

	public override void Execute(Cognition entity, double delta) {
		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		if (player == null)
			entity.StateMachine.ChangeState(AIState_Bandit_Fighting.Instance);

		// try attacking for a maximum amount of time only!
		if ((float)entity.Memory["attackDuration"] < 0f) {
			entity.StateMachine.ChangeState(AIState_Bandit_Fighting.Instance);
		}
		entity.Memory["attackDuration"] = (float)entity.Memory["attackDuration"] - (float)delta;

		// if we get close enough it counts as successfull attack for now
		if (entity.Vehicle.Position.DistanceTo(player.Position) <= 1.5) {
			GD.Print(entity.Root.Name + " ATTACKED: " + player.Name);
			entity.StateMachine.ChangeState(AIState_Bandit_Fighting.Instance);
		}
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit attacking");

		var seek = entity.Steering.Behaviours.OfType<Seek>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(seek);
	}
}