using System;
using System.Linq;
using Godot;

public class AIState_Wolf_Disengaging : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Disengaging> lazy = new(() => new AIState_Wolf_Disengaging());
	public static AIState_Wolf_Disengaging Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Disengaging");

		var target = entity.FocusTarget;

		var flee = new Flee(target, 15);
		entity.Steering.Behaviours.Add(flee);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;

		if (target == null) {
			return AIState_Wolf_Observing.Instance;
		}

		if (entity.Vehicle.Position.DistanceTo(target.Position) >= 15) {
			return AIState_Wolf_Observing.Instance;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Disengaging");

		var flee = entity.Steering.Behaviours.OfType<Flee>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(flee);
	}
}