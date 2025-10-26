using Godot;
using System;

public partial class Bullet : Area2D
{
	public float Speed = 3000f;
	private Vector2 _direction;
	private double _damage;
	private double _falloff;

	public void Init(double damage, double falloff, Vector2 direction)
	{
		_damage = damage;
		_falloff = falloff;
		_direction = direction.Normalized();
	}

	private void OnAreaEntered(Node body)
	{
		if (body.IsInGroup("Enemy")) 
		{
			body.CallDeferred("TakeDamage", _damage);
			despawn_bullet();
		}
	}
	
	private void OnBodyEntered(Node body)
	{
		if (body.IsInGroup("Enemy")) 
		{
			body.CallDeferred("TakeDamage", _damage);
			despawn_bullet();
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var sprite = GetNode<Sprite2D>("Sprite2D");

		var img = Image.Create(2, 2, false, Image.Format.Rgba8);
		img.Fill(Colors.Yellow);

		var tex = ImageTexture.CreateFromImage(img);
		sprite.Texture = tex;
		AreaEntered += OnAreaEntered;
		BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Transform.X * Speed * (float)delta;
	}
	
	private void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		despawn_bullet();
	}
	
	private void despawn_bullet()
	{
		GD.Print("Despawned bullet");
		QueueFree();
	}
}





