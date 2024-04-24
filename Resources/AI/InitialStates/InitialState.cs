using Godot;


[GlobalClass()]
public abstract partial class InitialState : Resource
{
	public abstract State<Cognition> GetInstance();
}