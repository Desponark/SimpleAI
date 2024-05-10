using System;

public class AIState_Wolf_Global : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Global> lazy = new(() => new AIState_Wolf_Global());
	public static AIState_Wolf_Global Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		MemorizePlayer(entity, delta);

		if (entity.GameplayStats.Fight > 0) {
			entity.GameplayStats.Fight -= (float)delta * 2;
		}
		else {
			entity.GameplayStats.Fight = 0;
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