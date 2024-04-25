using Godot;
using System;


/// <summary>
/// Returns a steering force that decelerates an agent onto a target position
/// </summary>
public class Arrive : SteeringBehaviour
{
	private float deceleration = 1;
	private Vector3 targetPos = Vector3.Zero;

	public Arrive(Vector3 targetPos, float deceleration = 1)
	{
		this.targetPos = targetPos;
		this.deceleration = deceleration;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta)
	{
		return Calc(vehicle, targetPos, deceleration);
	}

	public static Vector3 Calc(Vehicle vehicle, Vector3 targetPos, float deceleration)
	{
		var toTarget = targetPos - vehicle.Position;
		var dist = toTarget.Length();

		if (dist > 0.01)
		{
			var speed = dist / deceleration;
			speed = Math.Min(speed, vehicle.MaxSpeed);
			return toTarget * speed / dist;
		}
		return Vector3.Zero;
	}
}