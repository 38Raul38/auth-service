namespace AuthService.Application.Data.DTOs.Response;

public class AuthResponseDTO
{
    public string AccessToken { get; set; } = null!;
    public DateTime AccessTokenExpiresAt { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiresAt { get; set; }
    public UserResponseDTO User { get; set; } = null!;
} 