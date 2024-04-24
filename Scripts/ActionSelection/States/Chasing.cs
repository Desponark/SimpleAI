using System;
using System.Diagnostics;

public partial class Chasing : State<ActionSelection>
{
	private static readonly Lazy<Chasing> lazy = new(() => new Chasing());
	public static Chasing Instance { get { return lazy.Value; }}

	
	public override void Enter(ActionSelection entity)
	{
		Debug.WriteLine("Enter Chasing");
		
		var pursuit = (Pursuit)entity.steering.Behaviours.Find(x => x is Pursuit);
		if (pursuit != null) pursuit.active = true;

		entity.TargetVehicle = (Vehicle)entity.perception.Bodies.Find(x => x.IsInGroup("Player"));
	}

	public override void Execute(ActionSelection entity)
	{
		var player = entity.perception.Bodies.Find(x => x.IsInGroup("Player"));
		if (player == null)
		{
			entity.stateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
		}
	}

	public override void Exit(ActionSelection entity)
	{
		Debug.WriteLine("Exit Chasing");

		var pursuit = (Pursuit)entity.steering.Behaviours.Find(x => x is Pursuit);
		if (pursuit != null) pursuit.active = false;
	}
}