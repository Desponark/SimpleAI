using System;
using System.Linq;
using Godot;

public class AIState_Player_Controlling : State<Cognition> {
	private static readonly Lazy<AIState_Player_Controlling> lazy = new(() => new AIState_Player_Controlling());
	public static AIState_Player_Controlling Instance { get { return lazy.Value; } }


	public override void Enter(Cognition entity) {
		var input = entity.Root.GetChildren().OfType<PlayerInput>().FirstOrDefault();

		var arrive = new Arrive(input.TargetPos, 1);
		entity.Steering.Behaviours.Add(arrive);
	}

	public override State<Cognition> Execute(Cognition entity, double delta) {
		var input = entity.Root.GetChildren().OfType<PlayerInput>().FirstOrDefault();

		var arrive = entity.Steering.Behaviours.OfType<Arrive>().FirstOrDefault();
		arrive.SetTargetPos(input.TargetPos);

		DebugDrawer.DrawArrow(entity.Vehicle, entity.Vehicle.Position, input.TargetPos, 2, Colors.Blue);


		entity.Vehicle.SetCollisionLayerValue(5, input.IsPerceptionVisible);

		return null;
	}

	public override void Exit(Cognition entity) {
		var arrive = entity.Steering.Behaviours.OfType<Arrive>().FirstOrDefault();
		entity.Steering.Behaviours.Remove(arrive);
	}
}