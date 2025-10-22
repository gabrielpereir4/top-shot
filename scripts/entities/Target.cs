using Godot;
using System;

public partial class Target : Area2D
{
	[Export]
	public int Health = 20;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("Enemy");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void TakeDamage(int amount)
	{
		Health -= amount;
		GD.Print($"Target hit! Health: {Health}");

		// se quiser efeito visual rápido:
		Modulate = Colors.Purple;
		GetTree().CreateTimer(0.1f).Timeout += () => Modulate = Colors.White;

		if (Health <= 0)
			Die();
	}
	
	private void Die()
	{
		GetParent().Call("OnTargetDestroyed", GlobalPosition);
		QueueFree();
	}
}
