using Godot;
using System;

public partial class Character : CharacterBody2D
{
	[Export]
	public float Speed = 500f;

	[Export]
	public int MaxHealth = 100;

	public int Health;

	public override void _Ready()
	{
		Health = MaxHealth;
	}

	public virtual void Move(Vector2 direction)
	{
		Velocity = direction * Speed;
		MoveAndSlide();
	}

	public virtual void TakeDamage(int amount)
	{
		Health -= amount;
		if (Health <= 0)
			Die();
	}

	public virtual void Die()
	{
		// Maybe do something?
		QueueFree();
	}
}
