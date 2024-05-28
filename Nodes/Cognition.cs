using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// Instances the StateMachine and serves as the brain of the AI. It encapsulates all logic that represents thinking and decision making.
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

	public Dictionary<string, object> Memory = new(); // treating memory as short-term memory that is only being created and accessed in the same state is possibly a good idea for keeping a good overview
	public Vehicle FocusTarget;

	public override void _Ready() {
		Vehicle = (Vehicle)Root;
		Steering = Root.GetChildren().OfType<Steering>().FirstOrDefault();
		Perception = Root.GetChildren().OfType<Perception>().FirstOrDefault();
		GameplayStats = Root.GetChildren().OfType<GameplayStats>().FirstOrDefault();

		StateMachine = new StateMachine<Cognition>(this, InitialState.GetInstance(), InitialState.GetGlobalInstance());
	}

	public override void _Process(double delta) {
		StateMachine.Update(delta);
	}
}
