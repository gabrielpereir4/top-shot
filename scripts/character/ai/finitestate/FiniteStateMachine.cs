using Godot;
using System;

public abstract class FiniteStateMachine
{
	private FiniteState[] possibleStates;
	private FiniteState currentState;
	
	public FiniteStateMachine(FiniteState[] possibleStates)
	{
		this.possibleStates = possibleStates;
		this.currentState = possibleStates.Length > 0 ? possibleStates[0] : null;
	}
	
	/**
	* Verifies if FiniteState is valid before changing
	*/
	public void ChangeState(FiniteState newState)
	{
		if (Array.IndexOf(this.possibleStates, newState) == -1)
			throw new InvalidFiniteStateException();

		this.currentState = newState;
	}

	public FiniteState GetCurrentState() => this.currentState;
}
