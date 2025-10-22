using Godot;
using System;

public partial class Player : Character
{
	private Loadout loadout;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 input = new(
			Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
			Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
		);

		if (input != Vector2.Zero)
			input = input.Normalized();

		this.Move(input);
	}
	
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var mousePos = GetGlobalMousePosition();
		var direction = (mousePos - GlobalPosition).Normalized();

		// Rotates CharacterBody2D
		this.Rotation = direction.Angle();
	
		// TODO This input is looking weird, since I have to read it twice,
		// on the Shoot() method
		if (Input.IsActionPressed("shoot"))
			if (loadout != null)
				Shoot();

		if (Input.IsActionJustPressed("reload"))
			if (loadout != null)
				loadout.Reload();
	}
	
	private void Shoot(){
		var weapon = this.loadout?.weapon;
		var mousePos = GetGlobalMousePosition();
		var direction = (mousePos - GlobalPosition).Normalized();
		
		if (weapon != null)
		{
			if (weapon.FullAuto)
			{
				// enquanto segurar o botão, tente atirar — Trigger fará o rate limit
				if (Input.IsActionPressed("shoot"))
					loadout.Trigger(GlobalPosition, direction);
			}
			else
			{
				// semi-auto: atira só no clique
				if (Input.IsActionJustPressed("shoot"))
					loadout.Trigger(GlobalPosition, direction);
			}
		}
	}
	
	public override void _Input(InputEvent @event){
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if (keyEvent.Keycode == Key.Key1)
			{
				GD.Print("Key 1 pressed — reinitializing Pistol Loadout.");
				debug_loadout(true);
			}
			else if (keyEvent.Keycode == Key.Key2)
			{
				GD.Print("Key 2 pressed — reinitializing Rifle Loadout.");
				debug_loadout(false);
			}
		}
	}
	
	/*
	* Debug method
	*/
	private void debug_loadout(bool pistol){
		if (loadout != null){
			if (loadout.weapon != null){
				loadout.UnequipWeapon();
				return;
			}
		}
		if (loadout == null){
			loadout = new Loadout();
			AddChild(loadout);
			if (pistol)
				loadout.EquipWeapon("res://scenes/weapon/Pistol.tscn");
			else
				loadout.EquipWeapon("res://scenes/weapon/Rifle.tscn");
		} else {
			if (pistol)
				loadout.EquipWeapon("res://scenes/weapon/Pistol.tscn");
			else
				loadout.EquipWeapon("res://scenes/weapon/Rifle.tscn");
		}
			GD.Print("Rifle Loadout equipped!");
	}
	
	public override void Die(){
		GD.Print("O jogador morreu! Reiniciar jogo?");
		QueueFree();
	}
}
