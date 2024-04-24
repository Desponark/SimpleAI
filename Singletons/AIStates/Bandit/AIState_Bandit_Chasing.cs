using System;
using System.Diagnostics;

public partial class AIState_Bandit_Chasing : State<Cognition>
{
	private static readonly Lazy<AIState_Bandit_Chasing> lazy = new(() => new AIState_Bandit_Chasing());
	public static AIState_Bandit_Chasing Instance { get { return lazy.Value; }}

	
	public override void Enter(Cognition entity)
	{
		Debug.WriteLine("Enter Chasing");
		
		// var pursuit = (Pursuit)entity.steering.Behaviours.Find(x => x is Pursuit);
		// if (pursuit != null) pursuit.active = true;

		// entity.TargetVehicle = (Vehicle)entity.perception.Bodies.Find(x => x.IsInGroup("Player"));
	}

	public override void Execute(Cognition entity)
	{
		// var player = entity.perception.Bodies.Find(x => x.IsInGroup("Player"));
		// if (player == null)
		// {
		// 	entity.stateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
		// }
	}

	public override void Exit(Cognition entity)
	{
		Debug.WriteLine("Exit Chasing");

		// var pursuit = (Pursuit)entity.steering.Behaviours.Find(x => x is Pursuit);
		// if (pursuit != null) pursuit.active = false;
	}
}