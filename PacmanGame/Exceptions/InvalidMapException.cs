namespace PacmanGame.Exceptions;

/// <summary>
/// class <c>InvalidMapException</c> is the invalid map exception.
/// </summary>
public class InvalidMapException : Exception
{
    /// <summary>
    /// Constructor <c>InvalidMapException</c> is the constructor for the invalid map exception.
    /// </summary>
    /// <param name="message">The message of the exception.</param>
    public InvalidMapException(string message) : base(message){}
}