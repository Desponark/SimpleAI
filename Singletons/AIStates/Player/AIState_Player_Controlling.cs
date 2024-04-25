using System;
using System.Linq;

public class AIState_Player_Controlling : State<Cognition>
{	
	private static readonly Lazy<AIState_Player_Controlling> lazy = new(() => new AIState_Player_Controlling());
	public static AIState_Player_Controlling Instance { get { return lazy.Value; }}

	private Steering steering;
	private MouseInput mouseInput;

	private Seek seek;

	public override void Enter(Cognition entity)
	{
		steering = entity.Root.GetChildren().OfType<Steering>().FirstOrDefault();
		mouseInput = entity.Root.GetChildren().OfType<MouseInput>().FirstOrDefault();

		seek = new Seek(mouseInput.TargetPos);
		steering.Behaviours.Add(seek);
	}

	public override void Execute(Cognition entity)
	{
		seek.SetTargetPos(mouseInput.TargetPos);
	}

	public override void Exit(Cognition entity)
	{
		steering.Behaviours.Remove(seek);
	}
}