using AuthService.Application.Data.DTOs.Request;
using AuthService.Application.Data.DTOs.Response;

namespace AuthService.Application.Services.Interfaces;

using System.Threading;
using System.Threading.Tasks;
/*using AuthService.Application.Services.Models;*/

public interface IAccountService
{
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest.ChangePasswordRequestDTO request);
    Task<Result> ChangeEmailAsync(string userId, ChangeEmailRequest.ChangeEmailRequestDTO request);
}