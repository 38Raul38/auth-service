namespace AuthService.Core.Exceptions;

public sealed class RefreshTokenInvalidException : Exception
{
    public RefreshTokenInvalidException()
        : base("Refresh token is revoked or malformed.") { }
}