using System;

public class AIState_Empty : State<Cognition>
{
	private static readonly Lazy<AIState_Empty> lazy = new(() => new AIState_Empty());
	public static AIState_Empty Instance { get { return lazy.Value; }}

	public override void Enter(Cognition entity)
	{
	}

	public override void Execute(Cognition entity)
	{
	}

	public override void Exit(Cognition entity)
	{
	}
}
