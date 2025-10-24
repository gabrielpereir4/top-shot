using Godot;
using System;

public abstract class AIBehavior
{
	Character parent;
	
	public AIBehavior(Character character){
		this.parent = character;
	}
	
	public bool CanSeePlayer(){
		// implement vision detection?
		// this.parent.VisionCone
	}
	
	/**
	* Virtual because not every AI needs to have combat methods.
	*/
	public virtual void TacticalCombat(){
		// take cover, shoot with leaning...
	}
}
