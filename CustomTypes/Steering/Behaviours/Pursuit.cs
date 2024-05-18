using System;
using Godot;


/// <summary>
/// Returns a steering force that steers towards the predicted position of an agent
/// </summary>
public class Pursuit : SteeringBehaviour {
	private Vehicle evader;

	public Pursuit(Vehicle targetVehicle) {
		evader = targetVehicle;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		if (IsEvaderAheadAndFacingPursuer(vehicle))
			return Seek.Calc(vehicle, evader.Position);

		var lookAheadTime = vehicle.Position.DistanceTo(evader.Position) / (vehicle.MaxSpeed + evader.Velocity.Length());
		var predictedPos = evader.Position + evader.Velocity * lookAheadTime;

		return Seek.Calc(vehicle, predictedPos);
	}

	private bool IsEvaderAheadAndFacingPursuer(Vehicle vehicle) {
		var vehicleHeading = -vehicle.Transform.Basis.Z;
		var evaderHeading = -evader.Transform.Basis.Z;

		var relativeHeading = vehicleHeading.Dot(evaderHeading);
		var toEvader = evader.Position - vehicle.Position;
		if (toEvader.Dot(vehicleHeading) > 0 && relativeHeading < -0.95) //acos(0.95)=18 degs
			return true;
		return false;
	}
}
