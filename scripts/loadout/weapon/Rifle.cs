using Godot;
using System;

public partial class Rifle : Weapon
{
	[Export] public override double Damage { get; set; } = 90.0;
	[Export] public override double RangeDamageFalloff { get; set; } = 0.9;
	[Export] public override double FireRate { get; set; } = 8.0;
	[Export] public override bool FullAuto {get; set; } = true;
	[Export] public override int MagSize { get; set; } = 30;
	[Export] public override int CurrentMag {get; set; }
	[Export] public override double RecoilFactor { get; set; } = 50.0;
	[Export] public override double ReloadTime { get; set; }
	
}
