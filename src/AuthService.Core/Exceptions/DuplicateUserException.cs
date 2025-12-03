namespace AuthService.Core.Exceptions;

public sealed class DuplicateUserException : Exception
{
    public DuplicateUserException(string identifier)
        : base($"User with identifier '{identifier}' already exists.") { }
}