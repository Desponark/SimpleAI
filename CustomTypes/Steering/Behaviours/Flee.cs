using System;
using Godot;


/// <summary>
/// Returns a steering force that steers an agent away from the target position
/// </summary>
public class Flee : SteeringBehaviour {
	private Vector3 fixedTargetPos;
	private Vehicle targetVehicle;

	private double fleeDistance = 10;

	private Vector3 targetPos => targetVehicle != null ? targetVehicle.Position : fixedTargetPos;

	public Flee(Vector3 fixedTargetPos, double fleeDistance = 10) {
		this.fixedTargetPos = fixedTargetPos;
		this.fleeDistance = fleeDistance;
	}

	public Flee(Vehicle targetVehicle, double fleeDistance = 10) {
		this.targetVehicle = targetVehicle;
		this.fleeDistance = fleeDistance;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		if (vehicle.Position.DistanceTo(targetPos) > fleeDistance)
			return Vector3.Zero;

		return Calc(vehicle, targetPos);
	}

	public static Vector3 Calc(Vehicle vehicle, Vector3 from) {
		var desiredVelocity = from.DirectionTo(vehicle.Position) * vehicle.MaxSpeed;
		return desiredVelocity - vehicle.Velocity;
	}
}
