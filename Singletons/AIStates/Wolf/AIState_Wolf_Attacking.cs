using System;
using System.Linq;
using Godot;

public class AIState_Wolf_Attacking : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Attacking> lazy = new(() => new AIState_Wolf_Attacking());
	public static AIState_Wolf_Attacking Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Attacking");

		var target = entity.FocusTarget;

		var seek = new Seek(target);
		entity.Steering.Behaviours.Add(seek);

		entity.Vehicle.MaxSpeed *= 10;

		entity.Memory["attackDuration"] = 1.5f;
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;

		if (target == null) {
			return AIState_Wolf_Observing.Instance;
		}

		if ((float)entity.Memory["attackDuration"] < 0f) {
			return AIState_Wolf_Disengaging.Instance;
		}

		entity.Memory["attackDuration"] = (float)entity.Memory["attackDuration"] - (float)delta;

		if (entity.Vehicle.Position.DistanceTo(target.Position) <= 1.5) {
			GD.Print(entity.Root.Name + " ATTACKED " + target.Name);
			entity.GameplayStats.Fight -= 33;
			return AIState_Wolf_Disengaging.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Attacking");

		var seek = entity.Steering.Behaviours.OfType<Seek>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(seek);

		entity.Vehicle.MaxSpeed /= 10;
	}
}