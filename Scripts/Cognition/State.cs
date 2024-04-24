using System;
using Godot;

public abstract partial class State<T> : Node
{
	public abstract void Enter(T entity);
	public abstract void Execute(T entity);
	public abstract void Exit(T entity);
}
