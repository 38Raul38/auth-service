using System.Security.Claims;
using AuthService.Application.Data.DTOs.Request;
using AuthService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController: ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IAuthService _authService;
    
    public AccountController(IAccountService accountService, IAuthService authService)
    {
        _accountService = accountService;
        _authService = authService;
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest.ChangePasswordRequestDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var res = await _accountService.ChangePasswordAsync(userId, request);

        return Ok(res);
    }

    [Authorize]
    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmailAsync(ChangeEmailRequest.ChangeEmailRequestDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var res = await _accountService.ChangeEmailAsync(userId, request);

        return Ok(res);
    }
}