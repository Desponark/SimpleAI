using System;
using System.Diagnostics;
using System.Linq;

public class AIState_Bandit_Patrolling : State<Cognition>
{
	private static readonly Lazy<AIState_Bandit_Patrolling> lazy = new(() => new AIState_Bandit_Patrolling());
	public static AIState_Bandit_Patrolling Instance { get { return lazy.Value; }}


	private Steering steering;
	private Perception perception;
	private Wander wander;

	public override void Enter(Cognition entity)
	{
		Debug.WriteLine("Enter Patrol");
		
		steering = entity.Root.GetChildren().OfType<Steering>().FirstOrDefault();
		perception = entity.Root.GetChildren().OfType<Perception>().FirstOrDefault();

		wander = new Wander();
		steering.Behaviours.Add(wander);
	}

	public override void Execute(Cognition entity)
	{		
		var player = perception.Bodies.Find(x => x.IsInGroup("Player"));
		if (player != null)
		{
			entity.StateMachine.ChangeState(AIState_Bandit_Chasing.Instance);
		}
	}

	public override void Exit(Cognition entity)
	{
		Debug.WriteLine("Exit Patrol");
		
		steering.Behaviours.Remove(wander);
	}
}
