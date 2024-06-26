using System;
using System.Collections.Generic;
using Godot;


[GlobalClass]
public partial class Perception : Node3D {
	[Export]
	public Node3D Root;

	[Export]
	public Area3D Area3D;

	[Export]
	public RayCast3D[] WallFeelers = System.Array.Empty<RayCast3D>();

	private List<Node3D> perceptibleBodies = new();
	public List<Node3D> VisibleBodies = new();

	private List<Node3D> perceptibleAreas = new();
	public List<Node3D> Hearables = new();

	public override void _Ready() {
		Area3D.BodyEntered += BodyEntered;
		Area3D.BodyExited += BodyExited;
		Area3D.AreaEntered += AreaEntered;
		Area3D.AreaExited += AreaExited;
	}

	public override void _PhysicsProcess(double delta) {
		PopulateVisibleBodies(GetWorld3D().DirectSpaceState);

		// TODO: add filtering how things are heared
		Hearables = perceptibleAreas;
	}

	/// <summary>
	/// check if percebtibleBodies are actually visible and if yes add them to VisibleBodies
	/// </summary>
	/// <param name="spaceState"></param>
	private void PopulateVisibleBodies(PhysicsDirectSpaceState3D spaceState) {
		foreach (var body in perceptibleBodies) {
			var query = PhysicsRayQueryParameters3D.Create(GlobalPosition, body.GlobalPosition);
			var result = spaceState.IntersectRay(query);
			DebugDrawer.DrawArrow(this, GlobalPosition, (Vector3)result["position"], 1, Colors.Red);
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

	private void AreaEntered(Area3D area) {
		GD.Print("entered: " + area.Name);
		perceptibleAreas.Add(area);
	}

	private void AreaExited(Area3D area) {
		GD.Print("exited: " + area.Name);
		perceptibleAreas.Remove(area);
	}
}