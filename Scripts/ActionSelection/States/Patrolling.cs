using System;
using System.Diagnostics;

public partial class AIState_Bandit_Patrolling : State<ActionSelection>
{
	private static readonly Lazy<AIState_Bandit_Patrolling> lazy = new(() => new AIState_Bandit_Patrolling());
	public static AIState_Bandit_Patrolling Instance { get { return lazy.Value; }}


	public override void Enter(ActionSelection entity)
	{
		Debug.WriteLine("Enter Patrol");
		
		var wander = (Wander)entity.steering.Behaviours.Find(x => x is Wander);
		if (wander != null)	wander.active = true;
	}

	public override void Execute(ActionSelection entity)
	{
		// Debug.WriteLine("Execute Patrol");
		
		var player = entity.perception.Bodies.Find(x => x.IsInGroup("Player"));
		if (player != null)
		{
			entity.stateMachine.ChangeState(Chasing.Instance);
		}
	}

	public override void Exit(ActionSelection entity)
	{
		Debug.WriteLine("Exit Patrol");
		
		var wander = (Wander)entity.steering.Behaviours.Find(x => x is Wander);
		if (wander != null)	wander.active = false;
	}
}
