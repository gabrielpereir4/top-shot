public class FiniteState
{
	public string Name { get; }

	public FiniteState(string name)
	{
		Name = name;
	}

	public override string ToString() => Name;
}
