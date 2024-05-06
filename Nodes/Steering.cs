using Godot;
using System.Collections.Generic;


[GlobalClass]
public partial class Steering : Node {
	// Provides steering functionality for characters by iterating through assigned SteeringBehaviours and accumulating them into a steeringForce.
	// Currently applies to Godot-native CharacterBody3D, later to be replaced by custom character physics body.

	[Export]
	public Vehicle Vehicle;

	[Export]
	public double MaxSteeringForce = 10;

	public Vector3 SteeringForce { get; private set; }

	private List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();
	public List<SteeringBehaviour> Behaviours { get { return behaviours; } }

	public override void _PhysicsProcess(double delta) {
		CalculateBehaviours(delta);
	}

	private void CalculateBehaviours(double delta) {
		SteeringForce = Vector3.Zero;

		foreach (var behaviour in behaviours) {
			if (behaviour.Active) {
				if (!AccumulateForce(behaviour.Calculate(Vehicle, delta) * behaviour.Weight)) {
					return;
				}
			}
		}
	}

	private bool AccumulateForce(Vector3 addedForce) {
		double remainingMagnitude = MaxSteeringForce - SteeringForce.Length();

		if (remainingMagnitude <= 0) {
			return false;
		}
		else {
			double addedMagnitude = addedForce.Length();

			if (addedMagnitude < remainingMagnitude) {
				SteeringForce += addedForce;
			}
			else {
				SteeringForce += addedForce.Normalized() * (float)remainingMagnitude;
			}

			return true;
		}
	}
}
