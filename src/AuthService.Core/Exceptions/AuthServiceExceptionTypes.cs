namespace AuthService.Core.Exceptions;

public enum AuthServiceExceptionTypes
{
    INVALID_PASSWORD,
    USER_NOT_FOUND,
    TOKEN_EXPIRED,
    TOKEN_INVALID
}