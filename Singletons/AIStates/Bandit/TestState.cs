using System;
using System.Diagnostics;
using Godot;

public partial class TestState : State<Cognition>
{
	private static readonly Lazy<TestState> lazy = new(() => new TestState());
	public static TestState Instance { get { return lazy.Value; }}

	public override void Enter(Cognition t)
	{
		Debug.WriteLine("Enter TestState");
		// throw new NotImplementedException();
	}

	public override void Execute(Cognition t)
	{
		Debug.WriteLine("Execute TestState");
		// throw new NotImplementedException();
	}

	public override void Exit(Cognition t)
	{
		Debug.WriteLine("Exit TestState");
		// throw new NotImplementedException();
	}
}
