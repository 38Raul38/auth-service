namespace AuthService.Core.Exceptions;

public sealed class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
        : base("Provided credentials are invalid.") { }
}