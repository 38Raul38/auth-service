namespace AuthService.Application.Data.DTOs.Request;

public class ChangeEmailRequest
{
    public record ChangeEmailRequestDTO(string NewEmail, string Password);
}