using System;
using System.Linq;

public class AIState_Player_Controlling : State<Cognition> {
	private static readonly Lazy<AIState_Player_Controlling> lazy = new(() => new AIState_Player_Controlling());
	public static AIState_Player_Controlling Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		var mouseInput = entity.Root.GetChildren().OfType<MouseInput>().FirstOrDefault();

		var seek = new Seek(mouseInput.TargetPos);
		entity.Steering.Behaviours.Add(seek);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var mouseInput = entity.Root.GetChildren().OfType<MouseInput>().FirstOrDefault();

		var seek = entity.Steering.Behaviours.OfType<Seek>().FirstOrDefault();
		seek.SetTargetPos(mouseInput.TargetPos);

		return null;
	}

	public override void Exit(Cognition entity) {
		var seek = entity.Steering.Behaviours.OfType<Seek>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(seek);
	}
}