namespace AuthService.Core.Exceptions;

public sealed class AccountLockedException : Exception
{
    public AccountLockedException()
        : base("Account is locked due to policy violations.") { }
}