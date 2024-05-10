using System;
using System.Linq;
using Godot;

public class AIState_Wolf_Attacking : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Attacking> lazy = new(() => new AIState_Wolf_Attacking());
	public static AIState_Wolf_Attacking Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Attacking");

		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		var seek = new Seek(player);
		seek.Weight = 2;
		entity.Steering.Behaviours.Add(seek);

		entity.Memory["attackDuration"] = 2f;
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		if (player == null) {
			return AIState_Wolf_Observing.Instance;
		}
		if ((float)entity.Memory["attackDuration"] < 0f) {
			return AIState_Wolf_Disengaging.Instance;
		}
		entity.Memory["attackDuration"] = (float)entity.Memory["attackDuration"] - (float)delta;

		if (entity.Vehicle.Position.DistanceTo(player.Position) <= 1.5) {
			GD.Print(entity.Root.Name + " ATTACKED " + player.Name);
			entity.GameplayStats.Fight -= 33;
			return AIState_Wolf_Disengaging.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Attacking");

		var seek = entity.Steering.Behaviours.OfType<Seek>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(seek);
	}
}