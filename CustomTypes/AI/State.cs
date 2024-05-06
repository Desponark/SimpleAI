using System;
using Godot;

public abstract class State<T>
{
	public abstract void Enter(T entity);
	public abstract void Execute(T entity, double delta);
	public abstract void Exit(T entity);
}
