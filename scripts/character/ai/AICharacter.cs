using Godot;
using System;

/**
* Implements auxiliary AI Dynamic Character functions (seeing player, locating cover spots, future sound perception)
*/
public partial class AICharacter : Character
{
	[Export] public float Fov = 60f;
	[Export] public float VisionLength = 50f;
	[Export] public float FacingOffsetDegrees = 90f;

	protected Node2D player;
	
	public override void _Ready()
	{
		player = GetTree().Root.GetNode<Node2D>("Main/Player");

	}

	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	public override void _Draw()
	{
		// Converte FOV pra radianos
		float halfFovRad = Mathf.DegToRad(Fov / 2f);

		// Calcula vetores de borda do cone
		Vector2 leftBoundary = new Vector2(Mathf.Cos(Rotation - halfFovRad), Mathf.Sin(Rotation - halfFovRad)) * VisionLength;
		Vector2 rightBoundary = new Vector2(Mathf.Cos(Rotation + halfFovRad), Mathf.Sin(Rotation + halfFovRad)) * VisionLength;

		// Desenha o cone (preenchido semi-transparente)
		Vector2[] conePoints = { Vector2.Zero, leftBoundary, rightBoundary };
		Color coneColor = new Color(1, 1, 0, 0.15f); // amarelo translúcido
		DrawPolygon(conePoints, new Color[] { coneColor, coneColor, coneColor });

		// Opcional: linha central do olhar
		DrawLine(Vector2.Zero, (leftBoundary + rightBoundary) / 2, new Color(1, 1, 0), 1);
	}
	
		
	public bool CanSeePlayer()
	{
		if (player == null) return false;

		var space = GetWorld2D().DirectSpaceState;
		var toPlayer = player.GlobalPosition - GlobalPosition;
		float distanceToPlayer = toPlayer.Length();

		float angleToPlayer = Mathf.RadToDeg(Mathf.Atan2(toPlayer.Y, toPlayer.X));
		float facingAngle = RotationDegrees;
		float fov = 60f;

		float deltaAngle = Mathf.PosMod(angleToPlayer - facingAngle + 180f, 360f) - 180f;
		if (Mathf.Abs(deltaAngle) > fov / 2)
			return false;

		var query = PhysicsRayQueryParameters2D.Create(GlobalPosition, player.GlobalPosition);
		var result = space.IntersectRay(query);

		if (result.Count > 0 && result["collider"].AsGodotObject() == player)
			return true;

		return false;
	}

	public void LookToPlayer(double d){
		Vector2 toPlayer = player.GlobalPosition - GlobalPosition;
		float targetRotation = toPlayer.Angle();
		Rotation = Mathf.LerpAngle(Rotation, targetRotation, (float)(5f * d));
	}
	
	public void LocateCover(){
		// TODO
	}
}
