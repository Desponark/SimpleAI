using System;
using Godot;


/// <summary>
/// Returns a steering force that steers away from the predicted position of an agent
/// </summary>
public class Evade : SteeringBehaviour
{
	private double evadeDistance = 10;
	private Vehicle targetVehicle;

	public Evade(Vehicle targetVehicle, double evadeDistance = 10)
	{
		this.targetVehicle = targetVehicle;
		this.evadeDistance = evadeDistance;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta)
	{
		var pursuer = targetVehicle;

		var toPursuer = pursuer.Position.DistanceTo(vehicle.Position);
		if (toPursuer > evadeDistance)
			return Vector3.Zero;

		var lookAheadTime = toPursuer / (vehicle.MaxSpeed + pursuer.MaxSpeed);
		var predictedPos = pursuer.Position + pursuer.Velocity * lookAheadTime;
		return Seek.Calc(predictedPos, vehicle.Position, vehicle.MaxSpeed);
	}
}
