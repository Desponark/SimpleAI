using Godot;


[GlobalClass]
public partial class InitialState_BanditLeader : InitialState {
	public override State<Cognition> GetInstance() {
		return AIState_BanditLeader_Patrolling.Instance;
	}

	public override State<Cognition> GetGlobalInstance() {
		return AIState_BanditLeader_Global.Instance;
	}
}