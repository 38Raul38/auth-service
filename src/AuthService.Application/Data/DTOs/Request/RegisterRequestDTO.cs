using System.ComponentModel.DataAnnotations;

namespace AuthService.Application.Data.DTOs.Request;

public record RegisterRequestDTO(string Email, string FullName, string Password, string ConfirmPassword ); //string Surname, string Username,