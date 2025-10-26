using Godot;
using System;

/**
* Implements basic Dynamic Character physics and attributes (health, movement, leaning)
*/
public partial class Character : CharacterBody2D
{
	// Lean control
	public enum LeanDirection { None, Left, Right }
	protected LeanDirection lean = LeanDirection.None;
	protected LeanDirection previousLean = LeanDirection.None;
	private Vector2 leanOffsetLeft = new Vector2(0, -6);
	private Vector2 leanOffsetRight = new Vector2(0, 6);

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
	


	protected void ApplyLean()
	{
		if (lean == previousLean)
			return;
			
		// If we got here, lean state has changed
		switch (previousLean)
		// Erases previous lean offset
		{
			case LeanDirection.Left:
				GlobalPosition -= leanOffsetLeft.Rotated(Rotation);
				break;
			case LeanDirection.Right:
				GlobalPosition -= leanOffsetRight.Rotated(Rotation);
				break;
		}
	
		// Applies new lean offset (if necessary)
		switch (lean)
		{
			case LeanDirection.Left:
				GlobalPosition += leanOffsetLeft.Rotated(Rotation);
				break;
			case LeanDirection.Right:
				GlobalPosition += leanOffsetRight.Rotated(Rotation);
				break;
		}

		previousLean = lean;
	}
}
