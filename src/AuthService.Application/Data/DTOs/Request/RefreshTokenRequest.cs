using System.ComponentModel.DataAnnotations;

namespace AuthService.Application.Data.DTOs.Request;

public class RefreshTokenRequest
{
    [Required]
    public string AccessToken { get; set; } = null!;

    [Required]
    public string RefreshToken { get; set; } = null!;
}