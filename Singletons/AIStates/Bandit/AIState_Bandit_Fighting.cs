using System;
using System.Diagnostics;
using System.Linq;

public class AIState_Bandit_Fighting : State<Cognition>
{
	private static readonly Lazy<AIState_Bandit_Fighting> lazy = new(() => new AIState_Bandit_Fighting());
	public static AIState_Bandit_Fighting Instance { get { return lazy.Value; }}


	public override void Enter(Cognition entity)
	{
		Debug.WriteLine("Enter fighting");
		
		var vehicle = (Vehicle)entity.Memory["lastSeenPlayer"];

		var flee = new Flee(vehicle, 4);
		entity.Steering.Behaviours.Add(flee);

		var approach = new Approach(vehicle);
		entity.Steering.Behaviours.Add(approach);

		var orbit = new Orbit(vehicle, Random.Shared.NextDouble() >= 0.5);
		entity.Steering.Behaviours.Add(orbit);
	}

	public override void Execute(Cognition entity, double delta)
	{
		var player = (Vehicle)entity.Memory["lastSeenPlayer"];
		if (player == null)
		{
			entity.StateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
			return;
		} else
		{
			if (entity.Vehicle.Position.DistanceTo(player.Position) > 10)
			{
				entity.StateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
			}
		}

		// TODO: randomly attack
	}

	public override void Exit(Cognition entity)
	{
		Debug.WriteLine("Exit fighting");

		var flee = entity.Steering.Behaviours.OfType<Flee>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(flee);

		var approach = entity.Steering.Behaviours.OfType<Approach>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(approach);

		var orbit = entity.Steering.Behaviours.OfType<Orbit>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(orbit);
	}
}