using Godot;


[GlobalClass]
public partial class GameplayStats : Node {
	// gameplay stats node (general gameplay variables)
	// - health, stamina
	// - flight value
	// - fight value
	// - alert level
	// - disposition
	public float Flight;
	public float MaxFlight = 100;
	public float Fight;
	public float MaxFight = 100;
	public float Alert;
	public float MaxAlert = 100;
}
