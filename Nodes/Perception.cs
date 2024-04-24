using System.Collections.Generic;
using System.Diagnostics;
using Godot;


[GlobalClass]
public partial class Perception : Node3D
{
	[Export]
	public Area3D Area3D;

	public List<Node3D> Bodies = new();

	public override void _Ready()
	{
		Area3D.BodyEntered += BodyEntered;
		Area3D.BodyExited += BodyExited;
	}

	private void BodyEntered(Node3D body)
	{
		Bodies.Add(body);
		Debug.WriteLine("body entered: " + body);
		if (body.IsInGroup("Player")) {
			Debug.WriteLine("we have found the player: " + body);
		}
	}

	private void BodyExited(Node3D body)
	{
		Bodies.Remove(body);
		Debug.WriteLine("body exited: " + body);
	}
}