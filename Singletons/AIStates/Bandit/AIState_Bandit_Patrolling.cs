using System;
using System.Diagnostics;
using System.Linq;

public partial class AIState_Bandit_Patrolling : State<Cognition>
{
	private static readonly Lazy<AIState_Bandit_Patrolling> lazy = new(() => new AIState_Bandit_Patrolling());
	public static AIState_Bandit_Patrolling Instance { get { return lazy.Value; }}


	private Steering steering;
	private Perception perception;

	public override void Enter(Cognition entity)
	{
		Debug.WriteLine("Enter Patrol");
		
		steering = entity.Root.GetChildren().OfType<Steering>().FirstOrDefault();
		perception = entity.Root.GetChildren().OfType<Perception>().FirstOrDefault();


		var wander = steering.Behaviours.OfType<Wander>().FirstOrDefault();
		wander.active = true;
	}

	public override void Execute(Cognition entity)
	{
		// Debug.WriteLine("Execute Patrol");
		
		var player = perception.Bodies.Find(x => x.IsInGroup("Player"));
		if (player != null)
		{
			entity.StateMachine.ChangeState(AIState_Bandit_Chasing.Instance);
		}
	}

	public override void Exit(Cognition entity)
	{
		Debug.WriteLine("Exit Patrol");
		
		var wander = (Wander)steering.Behaviours.Find(x => x is Wander);
		if (wander != null)	wander.active = false;
	}
}
