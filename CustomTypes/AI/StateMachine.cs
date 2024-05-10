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

	public void Update(double delta) {
		globalState?.Execute(owner, delta);

		var newCurrentState = currentState?.Execute(owner, delta);
		if (newCurrentState != null)
			ChangeState(newCurrentState);
	}

	private void ChangeState(State<T> newState) {
		previousState = currentState;

		currentState.Exit(owner);

		currentState = newState ?? throw new Exception("Trying to change to a null state");

		currentState.Enter(owner);
	}

	public void RevertToPreviousState() {
		ChangeState(previousState);
	}

	public bool IsInState(State<T> st) {
		return currentState == st;
	}
}
