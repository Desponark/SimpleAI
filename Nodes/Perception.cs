using System.Collections.Generic;
using System.Diagnostics;
using Godot;


[GlobalClass]
public partial class Perception : Node3D {
	[Export]
	public Node3D Root;

	[Export]
	public Area3D Area3D;

	private List<Node3D> perceptibleBodies = new();
	public List<Node3D> VisibleBodies = new();

	public override void _Ready() {
		Area3D.BodyEntered += BodyEntered;
		Area3D.BodyExited += BodyExited;
	}

	public override void _PhysicsProcess(double delta) {
		PopulateVisibleBodies(GetWorld3D().DirectSpaceState);
	}

	/// <summary>
	/// check if percebtibleBodies are actually visible and if yes add them to VisibleBodies
	/// </summary>
	/// <param name="spaceState"></param>
	private void PopulateVisibleBodies(PhysicsDirectSpaceState3D spaceState) {
		foreach (var body in perceptibleBodies) {
			var query = PhysicsRayQueryParameters3D.Create(GlobalPosition, body.GlobalPosition);
			var result = spaceState.IntersectRay(query);
			if (result != null && body == (Node3D)result["collider"]) {
				if (!VisibleBodies.Contains(body)) {
					VisibleBodies.Add(body);
				}
				continue;
			}

			VisibleBodies.Remove(body);
		}
	}

	private void BodyEntered(Node3D body) {
		if (body != Root) {
			perceptibleBodies.Add(body);
		}
	}

	private void BodyExited(Node3D body) {
		perceptibleBodies.Remove(body);
		VisibleBodies.Remove(body);
	}
}