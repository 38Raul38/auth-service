using AuthService.Application.Data.DTOs.Request;
using AuthService.Application.Data.DTOs.Response;
using Result = FluentResults.Result;

namespace AuthService.Application.Services.Interfaces;

public interface IAuthService
{
    Task<Result> RegisterAsync(RegisterRequestDTO requestDTO);
    Task<TypeResult<AuthResponseDTO>> LoginAsync(LoginRequestDTO requestDTO);
    Task<TypeResult<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest requestDTO);
    Task<Result> LogoutAsync(LogoutRequestDTO requestDTO);
}