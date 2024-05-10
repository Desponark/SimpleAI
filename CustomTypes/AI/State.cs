using System;

public abstract class State<T> {
	public abstract void Enter(T entity);
	public abstract State<T> Execute(T entity, double delta);
	public abstract void Exit(T entity);
}
