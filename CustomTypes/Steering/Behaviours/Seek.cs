using System;
using Godot;


/// <summary>
/// Returns a steering force that directs an agent towards a target position
/// </summary>
public class Seek : SteeringBehaviour {
	private Vector3 fixedTargetPos = Vector3.Zero;
	private Vehicle targetVehicle = null;

	private Vector3 targetPos => targetVehicle != null ? targetVehicle.Position : fixedTargetPos;

	public Seek(Vector3 targetPos) {
		this.fixedTargetPos = targetPos;
	}

	public Seek(Vehicle targetVehicle) {
		this.targetVehicle = targetVehicle;
	}

	public void SetTargetPos(Vector3 targetPos) {
		this.fixedTargetPos = targetPos;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		return Calc(vehicle.Position, targetPos, vehicle.MaxSpeed);
	}

	public static Vector3 Calc(Vector3 from, Vector3 to, float maxSpeed) {
		return from.DirectionTo(to) * maxSpeed;
	}
}
