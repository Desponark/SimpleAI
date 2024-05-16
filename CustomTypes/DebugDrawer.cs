using System.Collections.Generic;
using System.Linq;
using Godot;

// heavily inspired by https://kidscancode.org/godot_recipes/3.x/3d/debug_overlay/index.html

public static class DebugDrawer {
	/// <summary>
	/// Draws an arrow from point a to b with specified width and color
	/// </summary>
	public static void DrawArrow(Node3D caller, Vector3 from, Vector3 to, float width = 1, Color? color = null) {

		var root = caller.GetTree().Root;

		var canvasLayer = root.GetChildren().OfType<CanvasLayer>().FirstOrDefault();
		if (canvasLayer == null) {
			canvasLayer = new CanvasLayer();
			root.CallDeferred(Node3D.MethodName.AddChild, canvasLayer);
		}

		var drawControl = canvasLayer.GetChildren().OfType<DrawControl>().FirstOrDefault();
		if (drawControl == null) {
			drawControl = new DrawControl();
			canvasLayer.CallDeferred(Node3D.MethodName.AddChild, drawControl);
		}

		Color c = color.GetValueOrDefault(Colors.Green);
		drawControl.AddArrow(from, to, width, c);
	}
}

public partial class DrawControl : Control {
	private List<DrawArrow> drawArrows = new();

	public override void _Draw() {
		var camera = GetViewport().GetCamera3D();
		foreach (var drawArrow in drawArrows) {
			drawArrow.Draw(this, camera);
		}

		drawArrows.Clear();
	}

	public override void _PhysicsProcess(double delta) {
		QueueRedraw();
	}

	public void AddArrow(Vector3 from, Vector3 to, float width, Color color) {
		drawArrows.Add(new DrawArrow(from, to, width, color));
	}
}

public class DrawArrow {
	private Vector3 from;
	private Vector3 to;
	private float width;
	private Color color;

	public DrawArrow(Vector3 from, Vector3 to, float width, Color color) {
		this.from = from;
		this.to = to;
		this.width = width;
		this.color = color;
	}

	public void Draw(DrawControl node, Camera3D camera) {
		var start = camera.UnprojectPosition(from);
		var end = camera.UnprojectPosition(to);
		node.DrawLine(start, end, color, width);
		drawTriangle(node, end, start.DirectionTo(end), width * 2, color);
	}

	private void drawTriangle(DrawControl node, Vector2 pos, Vector2 dir, float size, Color color) {
		var a = pos + dir * size;
		var b = pos + dir.Rotated(2 * Mathf.Pi / 3) * size;
		var c = pos + dir.Rotated(4 * Mathf.Pi / 3) * size;
		var points = new Vector2[] { a, b, c };
		var colors = new Color[] { color };
		if (!Geometry2D.TriangulatePolygon(points).IsEmpty())
			node.DrawPolygon(points, colors);
	}
}