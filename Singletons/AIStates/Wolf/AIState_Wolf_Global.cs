using System;
using Godot.Collections;
using System.Linq;
using Godot;

public class AIState_Wolf_Global : State<Cognition> {
	private static readonly Lazy<AIState_Wolf_Global> lazy = new(() => new AIState_Wolf_Global());
	public static AIState_Wolf_Global Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		UpdateFightValue(entity, delta);

		var threats = EvaluateThreats(entity);
		var highestThreat = GetHighestThreat(threats);
		Memorize(entity, delta, highestThreat, "highestThreat", 2);
		entity.FocusTarget = (Vehicle)entity.Memory["highestThreat"];

		return null;
	}

	private static void UpdateFightValue(Cognition entity, double delta) {
		if (entity.GameplayStats.Fight > 0) {
			entity.GameplayStats.Fight -= (float)delta * 2;
		}
		else {
			entity.GameplayStats.Fight = 0;
		}
	}

	private static Dictionary<Node3D, float> EvaluateThreats(Cognition entity) {
		var threats = new Dictionary<Node3D, float>();

		foreach (var body in entity.Perception.VisibleBodies) {
			// closer distance -> higher threat
			var distance = entity.Vehicle.Position.DistanceTo(body.Position);
			var threat = 100 / distance;

			// known enemy/threat type -> higher threat
			threat += body.IsInGroup("Bandit") ? 5 : 0;
			threat += body.IsInGroup("Player") ? 10 : 0;
			threat += body.IsInGroup("Deer") ? 15 : 0;
			threat += body.IsInGroup("Wolf") ? -100 : 0;

			// more code can be added for evaluating threats
			// e.g. took damage from other -> higher threat scaling with dmg

			threats[body] = threat;
		}

		return threats;
	}

	private static Node3D GetHighestThreat(Dictionary<Node3D, float> threats) {
		if (threats.Count <= 0)
			return null;

		var threat = threats.MaxBy(thr => thr.Value);

		if (threat.Value <= 0) // only consider targets with a threat value over 0 as threats
			return null;

		return threat.Key;
	}

	private static void Memorize(Cognition entity, double delta, Node3D memory, string memoryName, float memoryDuration) {
		var memoryTimer = memoryName + "Timer";

		if (memory != null) {
			entity.Memory[memoryName] = memory;
			entity.Memory[memoryTimer] = memoryDuration;
		}
		else {
			if (entity.Memory.TryGetValue(memoryTimer, out var value) && (float)value > 0) {
				entity.Memory[memoryTimer] = (float)value - (float)delta;
			}
			else {
				entity.Memory[memoryName] = null;
				entity.Memory[memoryTimer] = 0f;
			}
		}
	}

	public override void Exit(Cognition entity) {
	}
}