namespace AuthService.Core.Exceptions;

public sealed class PasswordExpiredException : Exception
{
    public PasswordExpiredException()
        : base("Password has expired and must be rotated.") { }
}