using Godot;
using System;

public partial class Loadout : Node2D
{
	public Weapon weapon {get; private set;}
	//public Vest vest {get; private set;}
	
	public void EquipWeapon(string weaponScenePath)
	{
		// Remove arma atual se houver
		UnequipWeapon();

		// Instancia nova arma
		var weaponScene = GD.Load<PackedScene>(weaponScenePath);
		var weaponInstance = weaponScene.Instantiate<Weapon>();
		AddChild(weaponInstance);
		weapon = weaponInstance;

		GD.Print($"Equipped weapon");
	}
	
	public void UnequipWeapon()
	{
		if (weapon != null)
		{
			GD.Print($"Unequipping weapon");
			weapon.QueueFree();
			weapon = null;
		}
	}
	
	public void Trigger(Vector2 origin, Vector2 direction)
	{
		weapon?.Trigger(origin, direction);
	}

	public void Reload()
	{
		weapon?.Reload();
	}
}
