using Godot;
using System;

public partial class Main : Node
{
	[Export] public PackedScene Target;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
		public void OnTargetDestroyed(Vector2 oldPosition)
	{
		GD.Print("Target destroyed, respawning...");
		// Espera um pouquinho antes de spawnar outro
		GetTree().CreateTimer(1.0f).Timeout += () => SpawnTarget(oldPosition);
	}

	private void SpawnTarget(Vector2 position)
	{
		var target = Target.Instantiate<Area2D>();
		target.Position = position;
		AddChild(target);
	}
}
