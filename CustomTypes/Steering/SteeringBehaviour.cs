using Godot;

public abstract class SteeringBehaviour {
	public bool Active = true;
	public float Weight = 1;

	public virtual Vector3 Calculate(Vehicle vehicle, double delta) {
		return Vector3.Zero;
	}
}
