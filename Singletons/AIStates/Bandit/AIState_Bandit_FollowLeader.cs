using System;
using System.Linq;
using Godot;

public class AIState_Bandit_FollowLeader : State<Cognition> {
	private static readonly Lazy<AIState_Bandit_FollowLeader> lazy = new(() => new AIState_Bandit_FollowLeader());
	public static AIState_Bandit_FollowLeader Instance { get { return lazy.Value; } }

	public override void Enter(Cognition entity) {
		GD.Print(entity.Root.Name + " Enter " + GetType().Name);

		// non final way of finding the commander
		var cmd = (Commanding)entity.Perception.Hearables.Find(x => x.GetParent() is Commanding).GetParent();

		// register self at leader
		cmd.Followers.Add(entity.Vehicle);

		var leader = (Vehicle)cmd.Root;
		var followDistance = cmd.Followers.Count * 4;

		var behaviour = new OffsetPursuit(leader, new Vector3(0, 0, followDistance));
		entity.Steering.Behaviours.Add(behaviour);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {


		return null;
	}

	public override void Exit(Cognition entity) {
		GD.Print(entity.Root.Name + " Exit " + GetType().Name);

		var behaviour = entity.Steering.Behaviours.OfType<OffsetPursuit>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(behaviour);
	}
}