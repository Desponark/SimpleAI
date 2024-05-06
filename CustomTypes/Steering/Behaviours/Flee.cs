using System;
using Godot;


/// <summary>
/// Returns a steering force that steers an agent away from the target position
/// </summary>
public class Flee : SteeringBehaviour
{
	private Vector3 targetPos;
	private Vehicle targetVehicle;
	
	private double fleeDistance = 10;
	
	public Flee(Vector3 targetPos, double fleeDistance = 10)
	{
		this.targetPos = targetPos;
		this.fleeDistance = fleeDistance;
	}

	public Flee(Vehicle targetVehicle, double fleeDistance = 10)
	{
		this.targetVehicle = targetVehicle;
		this.fleeDistance = fleeDistance;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta)
	{
		var pos = targetVehicle != null ? targetVehicle.Position : targetPos;

		if (vehicle.Position.DistanceTo(pos) > fleeDistance)
			return Vector3.Zero;
		
		return Seek.Calc(pos, vehicle.Position, vehicle.MaxSpeed);
	}
}
