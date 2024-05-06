using System;
using System.Linq;

public class AIState_Player_Controlling : State<Cognition> {
	private static readonly Lazy<AIState_Player_Controlling> lazy = new(() => new AIState_Player_Controlling());
	public static AIState_Player_Controlling Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		if (entity.Memory.TryGetValue("MouseInput", out var value) == false || value == null)
			entity.Memory["MouseInput"] = entity.Root.GetChildren().OfType<MouseInput>().FirstOrDefault();
		var mouseInput = (MouseInput)entity.Memory["MouseInput"];

		var seek = new Seek(mouseInput.TargetPos);
		entity.Steering.Behaviours.Add(seek);
	}

	public override void Execute(Cognition entity, double delta) {
		if (entity.Memory.TryGetValue("MouseInput", out var value) == false || value == null)
			entity.Memory["MouseInput"] = entity.Root.GetChildren().OfType<MouseInput>().FirstOrDefault();
		var mouseInput = (MouseInput)entity.Memory["MouseInput"];

		var seek = entity.Steering.Behaviours.OfType<Seek>().FirstOrDefault();
		seek.SetTargetPos(mouseInput.TargetPos);
	}

	public override void Exit(Cognition entity) {
		var seek = entity.Steering.Behaviours.OfType<Seek>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(seek);
	}
}