using System.Collections.Generic;
using System.Linq;
using Godot;

// heavily inspired by https://kidscancode.org/godot_recipes/3.x/3d/debug_overlay/index.html

/// <summary>
/// Provides easy access for drawing debug lines & arrows
/// </summary>
public static class DebugDrawer {
	public static void DrawVector(Node3D caller, Vector3 from, Vector3 to, float width = 1, Color? color = null) {

		var root = caller.GetTree().Root;

		var canvasLayer = root.GetChildren().OfType<CanvasLayer>().FirstOrDefault();
		if (canvasLayer == null) {
			canvasLayer = new CanvasLayer();
			root.CallDeferred(Node3D.MethodName.AddChild, canvasLayer);
		}

		var debugDraw3D = canvasLayer.GetChildren().OfType<DrawControl>().FirstOrDefault();
		if (debugDraw3D == null) {
			debugDraw3D = new DrawControl();
			root.CallDeferred(Node3D.MethodName.AddChild, debugDraw3D);
		}

		Color c = color.GetValueOrDefault(new Color(0, 1, 0));
		debugDraw3D.AddVector(from, to, width, c);
	}
}

/// <summary>
/// Draws all given elements for one frame
/// </summary>
public partial class DrawControl : Control {
	private List<DrawVector> debugDrawVectors = new();

	public override void _Draw() {
		var camera = GetViewport().GetCamera3D();
		foreach (var debugDrawVector in debugDrawVectors) {
			debugDrawVector.Draw(this, camera);
		}

		debugDrawVectors.Clear();
	}

	public override void _PhysicsProcess(double delta) {
		QueueRedraw();
	}

	public void AddVector(Vector3 from, Vector3 to, float width, Color color) {
		debugDrawVectors.Add(new DrawVector(from, to, width, color));
	}
}

/// <summary>
/// Data holder for each vector that should be drawn
/// </summary>
public class DrawVector {
	private Vector3 from;
	private Vector3 to;
	private float width;
	private Color color;

	public DrawVector(Vector3 from, Vector3 to, float width, Color color) {
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
		node.DrawPolygon(points, colors);
	}
}