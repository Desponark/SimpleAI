using System;
using Godot;


/// <summary>
/// /// Returns a steering force that steers towards the predicted position of an agent based on the last two positions it received
/// </summary>
public class PursuitByPositions : SteeringBehaviour {
	private Vector3 evaderVelocity = Vector3.Zero;

	private Vector3 lastKnowPos = Vector3.Zero;
	private Vector3 newestPos = Vector3.Zero;
	private double timeBetweenPosChange = 0;

	private Vector3 targetPos = Vector3.Zero;

	public PursuitByPositions(Vector3 targetPos) {
		this.targetPos = targetPos;
	}
	//TODO: add way to update targetPos

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		CalculateEvaderVelocity(delta);

		if (evaderVelocity == Vector3.Zero) {
			return Seek.Calc(vehicle.Position, targetPos, vehicle.MaxSpeed);
		}
		else {
			var toEvader = targetPos - vehicle.Position;

			if (IsEvaderAheadAndFacingPursuer(vehicle, toEvader))
				return Seek.Calc(vehicle.Position, targetPos, vehicle.MaxSpeed);

			var lookAheadTime = toEvader.Length() / (vehicle.MaxSpeed + evaderVelocity.Length());
			var predictedPos = targetPos + evaderVelocity * lookAheadTime;

			return Seek.Calc(vehicle.Position, predictedPos, vehicle.MaxSpeed);
		}
	}

	private bool IsEvaderAheadAndFacingPursuer(Vehicle vehicle, Vector3 toEvader) {
		var relativeHeading = vehicle.Velocity.Normalized().Dot(evaderVelocity.Normalized());
		if (toEvader.Dot(vehicle.Velocity.Normalized()) > 0 && relativeHeading < -0.95) //acos(0.95)=18 degs
			return true;
		return false;
	}

	private void CalculateEvaderVelocity(double delta) {
		timeBetweenPosChange += delta;

		if (newestPos != targetPos) {
			lastKnowPos = newestPos;
			newestPos = targetPos;
			if (lastKnowPos != Vector3.Zero) {
				evaderVelocity = lastKnowPos - newestPos / (float)timeBetweenPosChange;
			}
			timeBetweenPosChange = 0;
		}
	}
}
