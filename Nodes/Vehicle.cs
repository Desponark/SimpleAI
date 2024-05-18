using System.Diagnostics;
using Godot;


/// <summary>
/// Provides locomotion for a CharacterBody3D and some needed data so different steering behaviours can be tested.
/// </summary>
[GlobalClass]
public partial class Vehicle : CharacterBody3D {
	[Export]
	public Steering Steering;

	[Export]
	public int MaxSpeed = 10;

	[Export]
	public float Deceleration = 2;
	private readonly float gravityForce = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private readonly Vector3 gravityVector = ProjectSettings.GetSetting("physics/3d/default_gravity_vector").AsVector3();
	private Vector3 currentGravity = Vector3.Zero;

	public override void _PhysicsProcess(double delta) {
		var steeringForceMinusY = new Vector3(Steering.SteeringForce.X, 0, Steering.SteeringForce.Z);

		SimpleFriction(steeringForceMinusY);

		AddGravity(delta);

		Velocity += steeringForceMinusY * (float)delta;
		Velocity = Velocity.LimitLength(MaxSpeed);
		Velocity += currentGravity;

		DebugDrawer.DrawArrow(this, Position, Position + steeringForceMinusY, 2, Colors.AliceBlue);
		DebugDrawer.DrawArrow(this, Position, Position + Velocity, 2, Colors.DarkOrange);

		MoveAndSlide();
		LookAtVelocity();
	}

	private void SimpleFriction(Vector3 steeringForceMinusY) {
		if (steeringForceMinusY == Vector3.Zero) {
			Velocity *= 0.90f;
			if (Velocity.IsZeroApprox()) {
				Velocity = Vector3.Zero;
			}
		}
	}

	private void AddGravity(double delta) {
		if (!IsOnFloor()) {
			currentGravity += gravityVector * gravityForce * (float)delta;
		}
		else {
			currentGravity = Vector3.Zero;
		}
	}

	private void LookAtVelocity() {
		// LookAt doesn't like parallel vectors or zero vectors
		var isTooShort = Velocity.Length() < 0.01;
		var isTooStraightUpOrDown = Mathf.Abs(Velocity.Normalized().Dot(Vector3.Up)) > 0.9f;

		if (isTooShort || isTooStraightUpOrDown)
			return;

		LookAt(Position + Velocity);
	}

	private void LookAtInterpolated(Vector3 target) {
		Transform3D xForm = Transform;
		xForm = xForm.LookingAt(target, Vector3.Up);
		Transform = Transform.InterpolateWith(xForm, 0.05f);
	}
}
