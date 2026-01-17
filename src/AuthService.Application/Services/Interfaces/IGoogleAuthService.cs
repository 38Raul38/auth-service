using AuthService.Application.Data.DTOs.Response;

namespace AuthService.Application.Services.Interfaces;

public interface IGoogleAuthService
{
    Task<AuthResponseDTO> GoogleLoginAsync(string credential);
}