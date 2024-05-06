using Godot;

/// <summary>
/// Returns a steering force that steers an agent towards the target position up to a certain distance from it
/// </summary>
public class Approach : SteeringBehaviour {
	private Vector3 targetPos = Vector3.Zero;
	private Vehicle targetVehicle = null;

	private float distance = 6;

	public Approach(Vector3 targetPos, float distance = 6) {
		this.targetPos = targetPos;
	}

	public Approach(Vehicle targetVehicle, float distance = 6) {
		this.targetVehicle = targetVehicle;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		var pos = targetVehicle != null ? targetVehicle.Position : targetPos;

		if (vehicle.Position.DistanceTo(pos) < distance) {
			return Vector3.Zero;
		}

		return Seek.Calc(vehicle.Position, pos, vehicle.MaxSpeed);
	}
}
