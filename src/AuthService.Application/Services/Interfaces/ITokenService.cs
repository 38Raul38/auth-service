using System.Threading;
using System.Threading.Tasks;
using AuthService.Application.Data.DTOs.Request;
using AuthService.Application.Data.DTOs.Response;

namespace AuthService.Application.Services.Interfaces;

public interface ITokenService
{
    Task<AuthResponseDTO> CreateAsync(UserResponseDTO user, CancellationToken cancellationToken = default);
    Task<RefreshTokenResponse> RefreshAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task RevokeUserTokensAsync(string userId, CancellationToken cancellationToken = default);
}