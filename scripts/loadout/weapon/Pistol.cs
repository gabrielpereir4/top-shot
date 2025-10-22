using Godot;
using System;

public partial class Pistol : Weapon
{
	[Export] public override double Damage { get; set; } = 40.0;
	[Export] public override double RangeDamageFalloff { get; set; } = 0.9;
	[Export] public override double FireRate { get; set; } = 3.0;
	[Export] public override bool FullAuto {get; set; } = false;
	[Export] public override int MagSize { get; set; } = 12;
	[Export] public override int CurrentMag {get; set; }
	[Export] public override double RecoilFactor { get; set; } = 50.0;
	[Export] public override double ReloadTime { get; set; }
	
}
