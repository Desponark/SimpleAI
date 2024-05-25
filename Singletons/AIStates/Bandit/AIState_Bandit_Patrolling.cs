using System;
using System.Linq;
using Godot;

public class AIState_Bandit_Patrolling : State<Cognition> {
	private static readonly Lazy<AIState_Bandit_Patrolling> lazy = new(() => new AIState_Bandit_Patrolling());
	public static AIState_Bandit_Patrolling Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Patrol");

		var path3D = entity.Root.GetParent() as Path3D ?? throw new Exception("Parent node needs to be a Path3D");
		var points = path3D.Curve.GetBakedPoints();

		var followPath = new FollowPath(points);
		entity.Steering.Behaviours.Add(followPath);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var target = entity.FocusTarget;
		if (target != null) {
			return AIState_Bandit_Chasing.Instance;
		}

		foreach (var hearable in entity.Perception.Hearables) {
			if (hearable.GetParent() is Commanding commanding) {
				switch (commanding.CurrentCommand) {
					case Commanding.Command.Follow:
						return AIState_Bandit_FollowLeader.Instance;
				}
			}
		}

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Patrol");

		var followPath = entity.Steering.Behaviours.OfType<FollowPath>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(followPath);
	}
}
