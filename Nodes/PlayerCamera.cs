using Godot;


[GlobalClass]
public partial class PlayerCamera : Camera3D
{
	[Export]
	public Vehicle Player;

	private Vector3 distance = Vector3.Zero;

	public override void _Ready()
	{
		distance = Position;
	}

	public override void _Process(double delta)
	{
		if (Player != null)
		{
			Position = Player.Position + distance;
		}
	}
}
