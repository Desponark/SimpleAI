using System;
using System.Linq;
using Godot;

public class AIState_Deer_Fleeing : State<Cognition> {
	private static readonly Lazy<AIState_Deer_Fleeing> lazy = new(() => new AIState_Deer_Fleeing());
	public static AIState_Deer_Fleeing Instance { get { return lazy.Value; } }

	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Fleeing");

		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		var evade = new Evade(player, 25);
		entity.Steering.Behaviours.Add(evade);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var player = (Vehicle)entity.Memory["lastSeenPlayer"];

		if (player == null)
			return AIState_Deer_Watching.Instance;

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Fleeing");

		var evade = entity.Steering.Behaviours.OfType<Evade>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(evade);
	}
}