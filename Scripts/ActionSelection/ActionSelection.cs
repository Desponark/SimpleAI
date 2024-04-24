using Godot;


/// <summary>
/// Provides some control/decision logic so different steering behaviours can be tested.
/// Implementation not final and just for easy testing right now.
/// </summary>
[GlobalClass]
public partial class ActionSelection : Node
{
	[Export]
	public Vehicle Vehicle;

	[Export]
	public Vector3 TargetPos;

	[Export]
	public Vehicle TargetVehicle;


	public StateMachine<ActionSelection> stateMachine;

	[Export]
	public Steering steering;
	
	// TODO: make root property that is used by states -> states then search individually for whatever they need.
	[Export]
	public Perception perception;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		stateMachine = new StateMachine<ActionSelection>(this);
		stateMachine.SetCurrentState(TestState.Instance);

		stateMachine.ChangeState(AIState_Bandit_Patrolling.Instance);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		stateMachine.Update();
	}


	public override void _Input(InputEvent @event)
	{
		if (@event.IsAction("click"))
		{
			TargetPos = ScreenPointToRay();
		}
	}

	private Vector3 ScreenPointToRay()
	{
		var spaceState = Vehicle.GetWorld3D().DirectSpaceState;

		var mousePos = GetViewport().GetMousePosition();
		var camera = GetTree().Root.GetCamera3D();
		var rayOrig = camera.ProjectRayOrigin(mousePos);
		var rayEnd = rayOrig + camera.ProjectRayNormal(mousePos) * 2000;

		var rayParam = new PhysicsRayQueryParameters3D() {
			From = rayOrig,
			To = rayEnd
		};
		var rayRes = spaceState.IntersectRay(rayParam);
		
		if (rayRes.TryGetValue("position", out var position))
		{
			return (Vector3)position;
		}
		return Vector3.Zero;
	}
}
