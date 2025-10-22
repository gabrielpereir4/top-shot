using Godot;
using System;

public partial class Character : CharacterBody2D
{
	public enum LeanDirection { None, Left, Right }
	protected LeanDirection lean = LeanDirection.None;
	private Vector2 basePosition;
	private Vector2 leanOffsetLeft = new Vector2(0, -6);
	private Vector2 leanOffsetRight = new Vector2(0, 6);

	[Export]
	public float Speed = 500f;

	[Export]
	public int MaxHealth = 100;

	public int Health;

	public override void _Ready()
	{
		basePosition = GlobalPosition;
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
		GD.Print("leaning");
		var sprite = GetNode<ColorRect>("ColorRect");

		switch (lean)
		{
			case LeanDirection.Left:
				GlobalPosition = basePosition + leanOffsetLeft.Rotated(Rotation);
				break;
			case LeanDirection.Right:
				GlobalPosition = basePosition + leanOffsetRight.Rotated(Rotation);
				break;
			default:
				GlobalPosition = basePosition;
				break;
		}
	}
}
