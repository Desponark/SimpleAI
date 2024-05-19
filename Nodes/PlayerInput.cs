using System.Linq;
using Godot;


/// <summary>
/// 
/// </summary>
[GlobalClass]
public partial class PlayerInput : Node {
	[Export]
	public Vehicle Vehicle;

	public Vector3 TargetPos;

	public bool IsPerceptionVisible = true;


	private bool isDebugDrawerVisible = true;

	public override void _Input(InputEvent @event) {
		if (@event.IsActionReleased("click")) {
			TargetPos = ScreenPointToRay();
		}

		if (@event.IsActionReleased("changePerceptionVisibility")) {
			IsPerceptionVisible = !IsPerceptionVisible;
		}

		if (@event.IsActionReleased("debugDrawer")) {
			isDebugDrawerVisible = !isDebugDrawerVisible;
			DebugDrawer.ChangeVisibility(this, isDebugDrawerVisible);
		}
	}

	private Vector3 ScreenPointToRay() {
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

		if (rayRes.TryGetValue("position", out var position)) {
			return (Vector3)position;
		}
		return Vector3.Zero;
	}
}
