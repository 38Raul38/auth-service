namespace AuthService.Core.Exceptions;

public sealed class PermissionDeniedException : Exception
{
    public PermissionDeniedException(string scope)
        : base($"Permission denied for scope '{scope}'.") { }
}