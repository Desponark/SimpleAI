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

	public override void _Ready()
	{
		StateMachine = new StateMachine<Cognition>(this);
		StateMachine.SetCurrentState(TestState.Instance);

		StateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
	}

	public override void _Process(double delta)
	{
		StateMachine.Update();
	}
}
