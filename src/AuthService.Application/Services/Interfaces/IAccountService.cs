using AuthService.Application.Data.DTOs.Request;
using AuthService.Application.Data.DTOs.Response;

namespace AuthService.Application.Services.Interfaces;

using System.Threading;
using System.Threading.Tasks;
/*using AuthService.Application.Services.Models;*/

public interface IAccountService
{
    Task<Result> RegisterAsync(RegisterRequestDTO request, CancellationToken cancellationToken = default);
    Task<Result> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default);
    Task<Result> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task LogoutAsync(string userId, CancellationToken cancellationToken = default);
}
