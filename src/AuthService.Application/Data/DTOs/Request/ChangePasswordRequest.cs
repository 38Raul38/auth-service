namespace AuthService.Application.Data.DTOs.Request;

public class ChangePasswordRequest
{
    public record ChangePasswordRequestDTO(string CurrentPassword, string NewPassword, string ConfirmNewPassword);
}