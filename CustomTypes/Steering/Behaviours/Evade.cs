using System;
using Godot;


/// <summary>
/// Returns a steering force that steers away from the predicted position of an agent
/// </summary>
public class Evade : SteeringBehaviour {
	private double evadeDistance = 10;
	private Vehicle pursuer;

	public Evade(Vehicle pursuer, double evadeDistance = 10) {
		this.pursuer = pursuer;
		this.evadeDistance = evadeDistance;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		var distanceToPursuer = vehicle.Position.DistanceTo(pursuer.Position);
		if (distanceToPursuer > evadeDistance)
			return Vector3.Zero;

		var lookAheadTime = distanceToPursuer / (vehicle.MaxSpeed + pursuer.Velocity.Length());
		var predictedPos = pursuer.Position + pursuer.Velocity * lookAheadTime;
		return Flee.Calc(vehicle, predictedPos);
	}
}
