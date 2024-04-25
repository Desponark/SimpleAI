using System;
using System.Diagnostics;
using System.Linq;

public class AIState_Bandit_Chasing : State<Cognition>
{
	private static readonly Lazy<AIState_Bandit_Chasing> lazy = new(() => new AIState_Bandit_Chasing());
	public static AIState_Bandit_Chasing Instance { get { return lazy.Value; }}

	private Steering steering;
	private Perception perception;
	private Pursuit pursuit;

	public override void Enter(Cognition entity)
	{
		Debug.WriteLine("Enter Chasing");
		
		steering = entity.Root.GetChildren().OfType<Steering>().FirstOrDefault();
		perception = entity.Root.GetChildren().OfType<Perception>().FirstOrDefault();


		var vehicle = (Vehicle)perception.Bodies.Find(x => x.IsInGroup("Player"));
		
		pursuit = new Pursuit(vehicle);
		steering.Behaviours.Add(pursuit);
	}

	public override void Execute(Cognition entity)
	{
		var player = perception.Bodies.Find(x => x.IsInGroup("Player"));
		if (player == null)
		{
			entity.StateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
		}
	}

	public override void Exit(Cognition entity)
	{
		Debug.WriteLine("Exit Chasing");

		steering.Behaviours.Remove(pursuit);
	}
}