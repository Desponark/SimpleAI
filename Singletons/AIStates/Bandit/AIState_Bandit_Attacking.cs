using System;
using System.Linq;
using Godot;

public class AIState_Bandit_Attacking : State<Cognition> {
	private static readonly Lazy<AIState_Bandit_Attacking> lazy = new(() => new AIState_Bandit_Attacking());
	public static AIState_Bandit_Attacking Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter attacking");

		var target = entity.FocusTarget;

		var seek = new Seek(target);
		entity.Steering.Behaviours.Add(seek);

		entity.Memory["attackDuration"] = 4f;
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;

		if (target == null)
			return AIState_Bandit_Fighting.Instance;

		if ((float)entity.Memory["attackDuration"] < 0f) // only try attacking for a duration
			return AIState_Bandit_Fighting.Instance;

		entity.Memory["attackDuration"] = (float)entity.Memory["attackDuration"] - (float)delta;

		// simulate attack via print
		if (entity.Vehicle.Position.DistanceTo(target.Position) <= 1.5) {
			GD.Print(entity.Root.Name + " ATTACKED: " + target.Name);
			return AIState_Bandit_Fighting.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit attacking");

		var seek = entity.Steering.Behaviours.OfType<Seek>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(seek);
	}
}