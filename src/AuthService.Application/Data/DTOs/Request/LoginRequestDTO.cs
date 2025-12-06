using System.ComponentModel.DataAnnotations;

namespace AuthService.Application.Data.DTOs.Request;

public record LoginRequestDTO(string Email, string Password);