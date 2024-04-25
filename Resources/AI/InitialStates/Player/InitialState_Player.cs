using Godot;


[GlobalClass]
public partial class InitialState_Player : InitialState
{
	public override State<Cognition> GetInstance()
	{
		return AIState_Player_Controlling.Instance;
	}
}