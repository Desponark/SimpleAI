using System;
using System.Linq;
using Godot;

public class AIState_BanditLeader_Patrolling : State<Cognition> {
	private static readonly Lazy<AIState_BanditLeader_Patrolling> lazy = new(() => new AIState_BanditLeader_Patrolling());
	public static AIState_BanditLeader_Patrolling Instance { get { return lazy.Value; } }

	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter Patrol");

		var path3D = entity.Root.GetParent() as Path3D ?? throw new Exception("Parent node needs to be a Path3D");
		var points = path3D.Curve.GetBakedPoints();

		var followPath = new FollowPath(points);
		entity.Steering.Behaviours.Add(followPath);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		if (entity.FocusTarget != null)
			return null;

		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit Patrol");

		var followPath = entity.Steering.Behaviours.OfType<FollowPath>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(followPath);
	}
}