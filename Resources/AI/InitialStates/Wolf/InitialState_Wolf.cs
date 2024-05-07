using Godot;

[GlobalClass]
public partial class InitialState_Wolf : InitialState {
	public override State<Cognition> GetGlobalInstance() {
		return AIState_Wolf_Global.Instance;
	}

	public override State<Cognition> GetInstance() {
		return AIState_Wolf_Patrolling.Instance;
	}
}