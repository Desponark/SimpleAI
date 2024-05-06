using System;
using Godot;


/// <summary>
/// Returns a steering force required to keep an agent at a specified offset from a target agent.
/// </summary>
public class OffsetPursuit : SteeringBehaviour {
	private Vector3 offset = new(2, 0, 2);

	private Vehicle targetVehicle;

	public OffsetPursuit(Vehicle targetVehicle, Vector3 offset) {
		this.targetVehicle = targetVehicle;
		this.offset = offset;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		var leader = targetVehicle;

		var worldOffsetPos = leader.ToGlobal(offset);

		var lookAheadTime = vehicle.Position.DistanceTo(worldOffsetPos) / (vehicle.MaxSpeed + leader.MaxSpeed);
		var targetPos = worldOffsetPos + leader.Velocity * lookAheadTime;

		return Arrive.Calc(vehicle, targetPos, 0.1f);
	}
}
