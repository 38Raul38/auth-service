using System.ComponentModel.DataAnnotations;

namespace AuthService.Application.Data.DTOs.Request;

public record RegisterRequestDTO(string Email, string Name, string Surname, string Password, string ConfirmPassword );