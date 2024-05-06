using Godot;

[GlobalClass]
public partial class InitialState_Deer : InitialState
{
	public override State<Cognition> GetGlobalInstance()
	{
		return AIState_Deer_Global.Instance;
	}

	public override State<Cognition> GetInstance()
	{
		return AIState_Deer_Patrolling.Instance;
	}
}