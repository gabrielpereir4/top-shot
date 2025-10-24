using Godot;
using System;

public class EnemyFSM : FiniteStateMachine
{
	private enum State { Idle, Patrol, Attention, Combat }
	private State current = State.Idle;
	private Node2D player_reference;

	public EnemyFSM(Enemy parent) : base(parent)
	{
		player_reference = parent.GetTree().Root.GetNode<CharacterBody2D>("Root/Player");
	}
	
	public override void Update(double delta)
	{
		switch (current)
		{
			case State.Idle:
				if (parent.CanSeePlayer()){
					ChangeState(State.Attention)
				}
				break;

			case State.Attention:
				// 50% chance generator here
				// if chance > 0.5 parent.CarefulApproach()
				// else parent.GoToCoverSpot()
				break;

			case State.Combat:
				// 90% chance parent.TacticalCombat()
				// 10% chance parent.RecklessCombat()
				break;
		}
	}
	
	private void ChangeState(State newState)
	{
		// State change notifier
		parent.OnStateExit(current);
		current = newState;
		parent.OnStateEnter(current);
	}
}
