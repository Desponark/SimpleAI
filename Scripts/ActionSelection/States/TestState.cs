using System;
using System.Diagnostics;
using Godot;

public partial class TestState : State<ActionSelection>
{
	private static readonly Lazy<TestState> lazy = new(() => new TestState());
	public static TestState Instance { get { return lazy.Value; }}

	public override void Enter(ActionSelection t)
	{
		Debug.WriteLine("Enter TestState");
		// throw new NotImplementedException();
	}

	public override void Execute(ActionSelection t)
	{
		Debug.WriteLine("Execute TestState");
		// throw new NotImplementedException();
	}

	public override void Exit(ActionSelection t)
	{
		Debug.WriteLine("Exit TestState");
		// throw new NotImplementedException();
	}
}
