using System.ComponentModel.DataAnnotations;

namespace AuthService.Application.Data.DTOs.Request;

public class ForgotPasswordRequestDTO
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
}