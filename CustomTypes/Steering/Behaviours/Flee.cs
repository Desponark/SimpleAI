using System;
using Godot;


/// <summary>
/// Returns a steering force that steers an agent away from the target position
/// </summary>
public class Flee : SteeringBehaviour
{
	private double fleeDistance = 10;

	private Vector3 targetPos;
	
	public Flee(Vector3 targetPos, double fleeDistance = 10)
	{
		this.targetPos = targetPos;
		this.fleeDistance = fleeDistance;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta)
	{
		if (vehicle.Position.DistanceTo(targetPos) > fleeDistance)
			return Vector3.Zero;
		
		return Seek.Calc(targetPos, vehicle.Position, vehicle.MaxSpeed);
	}
}
