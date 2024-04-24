using Godot;


[GlobalClass]
public partial class InitialState_Bandit : InitialState
{
	public override State<Cognition> GetInstance()
	{
		return AIState_Bandit_Patrolling.Instance;
	}
}