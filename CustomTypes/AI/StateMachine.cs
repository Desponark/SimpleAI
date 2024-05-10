using System;

public class StateMachine<T> {
	private T owner;
	private State<T> currentState;
	private State<T> previousState;
	private State<T> globalState;

	public StateMachine(T owner, State<T> currentState, State<T> globalState = null) {
		this.owner = owner;
		this.currentState = currentState;
		this.previousState = null;
		this.globalState = globalState;

		ChangeState(this.currentState);
	}

	public void SetCurrentState(State<T> s) => currentState = s;
	public State<T> CurrentState() => currentState;

	public void SetPreviousState(State<T> s) => previousState = s;
	public State<T> PreviousState() => previousState;

	public void SetGlobalState(State<T> s) => globalState = s;
	public State<T> GlobalState() => globalState;


	public void Update(double delta) {
		var newGlobalState = globalState?.Execute(owner, delta);
		if (newGlobalState != null)
			ChangeState(newGlobalState);

		var newCurrentState = currentState?.Execute(owner, delta);
		if (newCurrentState != null)
			ChangeState(newCurrentState);
	}

	private void ChangeState(State<T> newState) {
		previousState = currentState;

		currentState.Exit(owner);

		currentState = newState ?? throw new System.Exception("Trying to change to a null state");

		currentState.Enter(owner);
	}

	public void RevertToPreviousState() {
		ChangeState(previousState);
	}

	public bool IsInState(State<T> st) {
		return currentState == st;
	}
}
