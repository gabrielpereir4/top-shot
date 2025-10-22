using Godot;
using System;

public abstract partial class Weapon : Node2D
{
	public abstract double Damage { get; set; } // Assumed close-quarters damage (full potential)
	public abstract double RangeDamageFalloff { get; set; } // Damage decrease by distance
	public abstract double FireRate { get; set; }
	public abstract bool FullAuto {get; set; }
	public abstract int MagSize { get; set; }
	public abstract int CurrentMag {get; set; }
	public abstract double ReloadTime { get; set; }
	public abstract double RecoilFactor { get; set; } // Factor for calculating spread
	
	[Export] public string BulletScenePath = "res://scenes/weapon/bullet/Bullet.tscn";
	
	private double _timeSinceLastShot = 0.0;
	
	public override void _Ready()
	{
		CurrentMag = MagSize;
	}
	
	public override void _Process(double delta){
		_timeSinceLastShot += delta;
	}
	
	public void Trigger(Vector2 position, Vector2 direction){
		// Before shooting, there is a need for bullets
		GD.Print("pulling trigger");
		if (CurrentMag == 0) {
			GD.Print("No ammo!");
			return; // TODO Maybe a click sound?
			}
		if (_timeSinceLastShot < 1.0 / FireRate)
			return;

		_timeSinceLastShot = 0.0;
		Fire(position, direction);
	}
	
	private void Fire(Vector2 position, Vector2 direction){
		var muzzle = GetNodeOrNull<Marker2D>("Muzzle");
		if (muzzle == null)
			return;
			
		var bulletScene = GD.Load<PackedScene>(BulletScenePath);
		var bullet = bulletScene.Instantiate<Area2D>();
		
		bullet.GlobalPosition = muzzle.GlobalPosition;
		bullet.Rotation = direction.Angle();
		
		if (bullet is Bullet b)
		{
			b.Init(Damage, RangeDamageFalloff, direction);
		}
		GetTree().Root.AddChild(bullet);
		this.CurrentMag -= 1;
	}
	
	public void Reload(){
		// play a sound?
		GD.Print("Reloading...");
		CurrentMag = MagSize;
	}
}
