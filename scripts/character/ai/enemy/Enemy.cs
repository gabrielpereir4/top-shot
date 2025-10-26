using Godot;
using System;

public partial class Enemy : AICharacter
{
	private EnemyFSM fsm = new();
	
	public override void _Ready()
	{
		GD.Print("Added");
		player = GetTree().Root.GetNode<Node2D>("Main/Player");
		AddToGroup("Enemy");
	}

	public override void _Process(double delta)
	{
		if (CanSeePlayer()){
			//GD.Print("I see you!!");
			LookToPlayer(delta);
		}
		/* 
		switch (fsm.GetCurrentState().Name)
		{
			case "Idle":
				PatrolIdleArea();
				if (CanSeePlayer())
					fsm.ChangeState(EnemyFSM.Combat);
				break;

			case "Combat":
				EngagePlayer();
				if (!CanSeePlayer())
					fsm.ChangeState(EnemyFSM.Patrol);
				break;

			case "Patrol":
				PatrolPath();
				if (CanSeePlayer())
					fsm.ChangeState(EnemyFSM.Combat);
				break;
		}
		*/
	}
	public override void Die()
	{
		GetParent().Call("onEnemyKilled", GlobalPosition);
		QueueFree();
	}
}
