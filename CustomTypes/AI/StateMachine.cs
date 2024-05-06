using System.Diagnostics;
using Godot;

public class StateMachine<T>
{
	private T owner;
	private State<T> currentState;
	private State<T> previousState;
	private State<T> globalState;

	public StateMachine(T owner)
	{
		this.owner = owner;
		currentState = null;
		previousState = null;
		globalState = null;
	}

	public void SetCurrentState(State<T> s) => currentState = s;
	public State<T> CurrentState() => currentState;

	public void SetPreviousState(State<T> s) => previousState = s;
	public State<T> PreviousState() => previousState;

	public void SetGlobalState(State<T> s) => globalState = s;
	public State<T> GlobalState() => globalState;
	

	public void Update(double delta)
	{
		if (globalState != null) globalState.Execute(owner, delta);

		if (currentState != null) currentState.Execute(owner, delta);
	}

	public void ChangeState(State<T> newState)
	{
		if (newState == null)
		{
			Debug.WriteLine("Trying to change to a null state");
			return;
		}

		previousState = currentState;

		currentState.Exit(owner);

		currentState = newState;

		currentState.Enter(owner);
	}

	public void RevertToPreviousState()
	{
		ChangeState(previousState);
	}

	public bool IsInState(State<T> st) {
		return currentState == st;
	}
}
