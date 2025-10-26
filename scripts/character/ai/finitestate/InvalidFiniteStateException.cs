using System;

public class InvalidFiniteStateException : Exception
{
	public InvalidFiniteStateException() 
		: base("Estado inválido para esta máquina de estados.") {}

	public InvalidFiniteStateException(string message) 
		: base(message) {}

	public InvalidFiniteStateException(string message, Exception innerException) 
		: base(message, innerException) {}
}
