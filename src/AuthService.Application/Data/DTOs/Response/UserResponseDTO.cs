namespace AuthService.Application.Data.DTOs.Response;

public class UserResponseDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public bool IsConfirmed { get; set; }
    public IReadOnlyCollection<string> Roles { get; set; } = Array.Empty<string>();
}