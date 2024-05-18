using Godot;
using System;


/// <summary>
/// Returns a steering force that decelerates an agent onto a target position
/// </summary>
public class Arrive : SteeringBehaviour {
	private float decelerationTweaker = 0.3f;
	private Vector3 fixedTargetPos = Vector3.Zero;
	private Vehicle targetVehicle;

	private Vector3 targetPos => targetVehicle != null ? targetVehicle.Position : fixedTargetPos;

	public Arrive(Vector3 targetPos, float decelerationTweaker = 0.3f) {
		this.fixedTargetPos = targetPos;
		this.decelerationTweaker = decelerationTweaker;
	}

	public Arrive(Vehicle targetVehicle, float decelerationTweaker = 0.3f) {
		this.targetVehicle = targetVehicle;
		this.decelerationTweaker = decelerationTweaker;
	}

	public void SetTargetPos(Vector3 pos) {
		this.fixedTargetPos = pos;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		return Calc(vehicle, targetPos, decelerationTweaker);
	}

	public static Vector3 Calc(Vehicle vehicle, Vector3 targetPos, float decelerationTweaker) {
		var toTarget = targetPos - vehicle.Position;
		var dist = toTarget.Length();

		if (dist > 0) {
			var speed = dist / (vehicle.Deceleration * decelerationTweaker);

			speed = Math.Min(speed, vehicle.MaxSpeed);

			var desiredVelocity = toTarget * speed / dist;

			return desiredVelocity - vehicle.Velocity;
		}
		return Vector3.Zero;
	}
}
