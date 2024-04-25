using System;
using System.Diagnostics;
using System.Linq;

public class AIState_Bandit_Chasing : State<Cognition>
{
	private static readonly Lazy<AIState_Bandit_Chasing> lazy = new(() => new AIState_Bandit_Chasing());
	public static AIState_Bandit_Chasing Instance { get { return lazy.Value; }}


	public override void Enter(Cognition entity)
	{
		Debug.WriteLine("Enter Chasing");
		
		var vehicle = (Vehicle)entity.Perception.Bodies.Find(x => x.IsInGroup("Player"));
		
		var pursuit = new Pursuit(vehicle);
		entity.Steering.Behaviours.Add(pursuit);
	}

	public override void Execute(Cognition entity)
	{
		var player = entity.Perception.Bodies.Find(x => x.IsInGroup("Player"));
		if (player == null)
		{
			entity.StateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
		}
	}

	public override void Exit(Cognition entity)
	{
		Debug.WriteLine("Exit Chasing");

		var pursuit = entity.Steering.Behaviours.OfType<Pursuit>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(pursuit);
	}
}