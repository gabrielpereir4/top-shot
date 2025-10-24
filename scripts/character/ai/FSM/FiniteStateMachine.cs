using Godot;
using System;

public abstract class FiniteStateMachine
{
	protected Character parent;
	public abstract void Update(double delta);
	
	public FiniteStateMachine(Character parent){
		this.parent = parent;
	}
}
