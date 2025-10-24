using Godot;
using System;

/**
* Class responsible for atributing the respective FSM to the given AI character.
*/
public class AIController
{
	private Character parent;
	
	public AIController(Character characterParent){
		// Constructor, thinking about using enum based on parent class 
		// to define a FSM.
		this.parent = characterParent;
		if(this.parent is Enemy){
			
		}
	}
	
	// TODO
	public void onStateEnter(){
		return;
	}
	
	// TODO
	public void onStateExit(){
		return;
	}
}
