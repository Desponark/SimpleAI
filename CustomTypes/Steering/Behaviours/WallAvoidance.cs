using System;
using Godot;

/// <summary>
/// Returns a steering force that steers an agent away from walls via an array of feelers
/// </summary>
public class WallAvoidance : SteeringBehaviour {
	private readonly RayCast3D[] obstacleFeelers = Array.Empty<RayCast3D>();

	public WallAvoidance(RayCast3D[] feelers) {
		this.obstacleFeelers = feelers;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		var steeringForce = Vector3.Zero;

		foreach (var feeler in obstacleFeelers) {
			if (feeler.IsColliding()) {
				var point = feeler.GetCollisionPoint();
				var normal = feeler.GetCollisionNormal();

				var raycastEndPoint = feeler.GlobalPosition + feeler.TargetPosition;
				steeringForce += normal * point.DistanceTo(raycastEndPoint);
			}
		}
		DebugDrawer.DrawArrow(vehicle, vehicle.Position, vehicle.Position + steeringForce, 2, Colors.Violet);

		return steeringForce;
	}
}