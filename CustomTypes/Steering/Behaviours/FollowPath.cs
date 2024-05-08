using Godot;

/// <summary>
/// Returns a steering force that moves an agent along a series of positions
/// </summary>
public class FollowPath : SteeringBehaviour {

	private Vector3[] waypoints;
	private bool looping = true;
	private int waypointDistance = 2;
	private int index = 0;


	public FollowPath(Vector3[] waypoints, bool looping = true, int waypointDistance = 2) {
		this.waypoints = waypoints;
		this.looping = looping;
		this.waypointDistance = waypointDistance;
	}


	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		if (vehicle.Position.DistanceTo(waypoints[index]) <= waypointDistance) {
			index++;
		}

		if (index >= waypoints.Length) {
			if (looping) {
				index = 0;
			}
			return Vector3.Zero;
		}

		return Seek.Calc(vehicle.Position, waypoints[index], vehicle.MaxSpeed);
	}
}
