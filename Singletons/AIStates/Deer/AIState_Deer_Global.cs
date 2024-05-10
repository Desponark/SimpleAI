using System;
using Godot;


public class AIState_Deer_Global : State<Cognition> {
	private static readonly Lazy<AIState_Deer_Global> lazy = new(() => new AIState_Deer_Global());
	public static AIState_Deer_Global Instance { get { return lazy.Value; } }

	public override void Enter(Cognition entity) {
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		MemorizePlayer(entity, delta);

		if (entity.GameplayStats.Flight > 0) {
			entity.GameplayStats.Flight -= (float)delta * 2;
		}
		else {
			entity.GameplayStats.Flight = 0;
		}

		return null;
	}

	public override void Exit(Cognition entity) {
	}

	private static void MemorizePlayer(Cognition entity, double delta) {
		var player = entity.Perception.VisibleBodies.Find(x => x.IsInGroup("Player"));

		if (player != null) {
			entity.Memory["lastSeenPlayer"] = player;
			entity.Memory["lastSeenTimer"] = 2f;
		}
		else {
			if (entity.Memory.TryGetValue("lastSeenTimer", out var value) && (float)value > 0) {
				entity.Memory["lastSeenTimer"] = (float)value - (float)delta;
			}
			else {
				entity.Memory["lastSeenPlayer"] = null;
				entity.Memory["lastSeenTimer"] = 0f;
			}
		}
	}
}