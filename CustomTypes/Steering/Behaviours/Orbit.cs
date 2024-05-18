using System;
using Godot;


/// <summary>
/// Returns a steering force that steers an agent in an orbit around the target
/// </summary>
public class Orbit : SteeringBehaviour {
	private Vehicle targetVehicle;
	private Vector3 fixedOrbitTarget;
	private bool orbitClockwise;
	private float orbitDistance;

	private float rotdegree = Mathf.DegToRad(15);
	private float rotation => orbitClockwise ? rotdegree : -rotdegree;
	private Vector3 orbitTarget => targetVehicle != null ? targetVehicle.Position : fixedOrbitTarget;

	public Orbit(Vector3 fixedOrbitTarget, bool orbitClockwise = true, float orbitDistance = 10) {
		this.fixedOrbitTarget = fixedOrbitTarget;
		this.orbitClockwise = orbitClockwise;
		this.orbitDistance = orbitDistance;
	}

	public Orbit(Vehicle targetVehicle, bool orbitClockwise = true, float orbitDistance = 10) {
		this.targetVehicle = targetVehicle;
		this.orbitClockwise = orbitClockwise;
		this.orbitDistance = orbitDistance;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		var toVehicleDir = orbitTarget.DirectionTo(vehicle.Position);

		var rotatedDir = toVehicleDir.Rotated(Vector3.Up, rotation);

		var orbitDir = rotatedDir * orbitDistance;
		var orbit = orbitTarget + orbitDir;

		DebugDrawer.DrawArrow(vehicle, orbitTarget, orbit);
		return Seek.Calc(vehicle, orbit);
	}
}
