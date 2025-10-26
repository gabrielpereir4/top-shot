public class EnemyFSM : FiniteStateMachine
{
	public static readonly FiniteState Idle = new("Idle");
	public static readonly FiniteState Patrol = new("Patrol");
	public static readonly FiniteState Combat = new("Combat");

	// Provides FiniteStateMachine with the possible states for the Enemy type.
	public EnemyFSM() : base(new FiniteState[] { Idle, Patrol, Combat })
	{
	}
}
