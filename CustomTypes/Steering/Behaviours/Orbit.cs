using Godot;


/// <summary>
/// Returns a steering force that steers an agent in an orbit around the target
/// </summary>
public class Orbit : SteeringBehaviour {
	private Vector3 orbitTarget;
	private bool orbitClockwise = true;

	private Vehicle targetVehicle;

	public Orbit(Vector3 orbitTarget, bool orbitClockwise = true) {
		this.orbitTarget = orbitTarget;
		this.orbitClockwise = orbitClockwise;
	}

	public Orbit(Vehicle targetVehicle, bool orbitClockwise = true) {
		this.targetVehicle = targetVehicle;
		this.orbitClockwise = orbitClockwise;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		var target = targetVehicle != null ? targetVehicle.Position : orbitTarget;

		var rotation = orbitClockwise ? Mathf.DegToRad(90) : Mathf.DegToRad(-90);
		var orbit = vehicle.Position.DirectionTo(target).Rotated(Vector3.Up, rotation);
		return orbit * vehicle.MaxSpeed;
	}
}
