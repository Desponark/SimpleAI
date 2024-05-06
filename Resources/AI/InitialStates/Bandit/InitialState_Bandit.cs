using Godot;


[GlobalClass]
public partial class InitialState_Bandit : InitialState {
	public override State<Cognition> GetInstance() {
		return AIState_Bandit_Patrolling.Instance;
	}

	public override State<Cognition> GetGlobalInstance() {
		return AIState_Bandit_Global.Instance;
	}
}