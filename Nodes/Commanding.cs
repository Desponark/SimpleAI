using Godot;
using System;
using System.Collections.Generic;

public partial class Commanding : Node3D {
	[Export]
	public Node3D Root;


	public Vehicle Target = null; // null = everyone;

	public List<Vehicle> Followers = new();

	public Command CurrentCommand = Command.Follow;

	public enum Command {
		None,
		Stop,
		Follow,
		Attack
	}
}