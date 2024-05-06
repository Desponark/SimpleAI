using System.Diagnostics;
using Godot;


/// <summary>
/// Provides locomotion for a CharacterBody3D and some needed data so different steering behaviours can be tested.
/// </summary>
[GlobalClass]
public partial class Vehicle : CharacterBody3D
{
	[Export]
	public Steering Steering;

	[Export]
	public int MaxSpeed = 10;

	[Export]
	public float Deceleration = 1;

	private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private Vector3 gravityVector = ProjectSettings.GetSetting("physics/3d/default_gravity_vector").AsVector3();

	public override void _PhysicsProcess(double delta)
	{
		// LookAt doesn't like parallel vectors or zero vectors
		if (false == (Position.Normalized().Abs().IsEqualApprox(Steering.SteeringForce.Normalized().Abs())
			|| Steering.SteeringForce.IsEqualApprox(Vector3.Zero)))
		{
			LookAt(Position + new Vector3(Steering.SteeringForce.X, 0, Steering.SteeringForce.Z));
		}

		Velocity = new Vector3(Steering.SteeringForce.X, 0, Steering.SteeringForce.Z);
		Velocity += gravityVector * gravity;
		MoveAndSlide();
	}
}
