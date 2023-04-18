namespace PacmanGame.Exceptions;
public class InvalidMapException : Exception
{
    public InvalidMapException(string message) : base(message){}
}