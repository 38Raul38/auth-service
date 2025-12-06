using System.ComponentModel.DataAnnotations;

namespace AuthService.Application.Data.DTOs.Request;

public class LogoutRequestDTO
{
    [Required]
    public string RefreshToken { get; set; } = null!;
}
