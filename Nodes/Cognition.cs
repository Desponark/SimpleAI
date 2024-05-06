using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// Brain
/// </summary>
[GlobalClass]
public partial class Cognition : Node {
	[Export]
	public Node Root;
	[Export]
	public InitialState InitialState;

	public Vehicle Vehicle;
	public Steering Steering;
	public Perception Perception;
	public GameplayStats GameplayStats;
	public StateMachine<Cognition> StateMachine;

	public Dictionary<string, object> Memory = new();

	public override void _Ready() {
		Vehicle = (Vehicle)Root;
		Steering = Root.GetChildren().OfType<Steering>().FirstOrDefault();
		Perception = Root.GetChildren().OfType<Perception>().FirstOrDefault();
		GameplayStats = Root.GetChildren().OfType<GameplayStats>().FirstOrDefault();

		StateMachine = new StateMachine<Cognition>(this);
		StateMachine.SetGlobalState(InitialState.GetGlobalInstance());
		StateMachine.SetCurrentState(AIState_Empty.Instance);
		StateMachine.ChangeState(InitialState.GetInstance());
	}

	public override void _Process(double delta) {
		StateMachine.Update(delta);
	}
}
