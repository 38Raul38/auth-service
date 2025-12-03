namespace AuthService.Core.Exceptions;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(string identifier)
        : base($"User with identifier '{identifier}' was not found.") { }
}