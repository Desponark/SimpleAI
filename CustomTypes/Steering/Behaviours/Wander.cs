using System;
using Godot;


/// <summary>
/// Returns a steering force that makes an agent wander around
/// by projecting a circle in front of it and steering towards
/// a target position that is constrained to move along the circle perimeter.
/// </summary>
public class Wander : SteeringBehaviour {
	private float wanderRadius;
	private float wanderDistance;
	private float wanderJitter;
	private Vector3 wanderTarget = Vector3.Zero;

	public Wander(float wanderRadius = 5, float wanderDistance = 10, float wanderJitter = 1) {
		this.wanderRadius = wanderRadius;
		this.wanderDistance = wanderDistance;
		this.wanderJitter = wanderJitter;
	}

	public override Vector3 Calculate(Vehicle vehicle, double delta) {
		if (wanderTarget == Vector3.Zero)
			wanderTarget = new Vector3(vehicle.Position.X, 0, vehicle.Position.Z);

		wanderTarget += new Vector3(RandomUnitFloat() * wanderJitter, 0, RandomUnitFloat() * wanderJitter);

		wanderTarget = wanderTarget.Normalized();
		wanderTarget *= wanderRadius;

		var targetLocal = wanderTarget + Vector3.Forward * wanderDistance;
		var targetWorldPos = vehicle.ToGlobal(targetLocal);

		DebugDrawer.DrawArrow(vehicle, vehicle.Position, targetWorldPos, 2, Colors.Blue);
		DebugDrawer.DrawArrow(vehicle, vehicle.ToGlobal(Vector3.Forward * wanderDistance), targetWorldPos, 1);

		return Seek.Calc(vehicle, targetWorldPos);
	}

	private static float RandomUnitFloat() {
		var rand = new RandomNumberGenerator();
		return rand.RandfRange(-1, 1);
	}
}
