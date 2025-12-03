namespace AuthService.Core.Exceptions;

public sealed class TokenExpiredException : Exception
{
    public TokenExpiredException()
        : base("Access token lifetime has elapsed.") { }
}