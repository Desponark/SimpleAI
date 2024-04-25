using Godot;


/// <summary>
/// Brain
/// </summary>
[GlobalClass]
public partial class Cognition : Node
{
	[Export]
	public Node Root;

	public StateMachine<Cognition> StateMachine;

	[Export]
	public InitialState InitialState;

	public override void _Ready()
	{
		StateMachine = new StateMachine<Cognition>(this);
		StateMachine.SetCurrentState(AIState_Empty.Instance);
		StateMachine.ChangeState(InitialState.GetInstance());
	}

	public override void _Process(double delta)
	{
		StateMachine.Update();
	}
}
