using System;
using System.Linq;
using Godot;

public class AIState_Deer_Patrolling : State<Cognition>
{
	private static readonly Lazy<AIState_Deer_Patrolling> lazy = new(() => new AIState_Deer_Patrolling());
	public static AIState_Deer_Patrolling Instance { get { return lazy.Value; }}


	public override void Enter(Cognition entity)
	{
		GD.Print("Enter Patrol");
		
		var wander = new Wander();
		wander.Weight = 0.5f;
		entity.Steering.Behaviours.Add(wander);
	}

	public override void Execute(Cognition entity, double delta)
	{
		var player = entity.Perception.VisibleBodies.Find(x => x.IsInGroup("Player"));
		if (player != null)
		{
			entity.StateMachine.ChangeState(AIState_Deer_Watching.Instance);
		}
	}

	public override void Exit(Cognition entity)
	{
		GD.Print("Exit Patrol");

		var wander = entity.Steering.Behaviours.OfType<Wander>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(wander);
	}
}