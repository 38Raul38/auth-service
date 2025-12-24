using AuthService.Application.Data.DTOs.Request;
using AuthService.Application.Data.DTOs.Response;
using AuthService.Application.Services.Interfaces;
using AuthService.Application.Utils;
using AuthService.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequestDTO request)
    {
        var res = await _authService.RegisterAsync(request);

        return Ok(res); //можно реализовать более точную обработку кода статуса
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequestDTO request)
    {
        var res = await _authService.LoginAsync(request);
        
        return Ok(res);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var res = await _authService.RefreshTokenAsync(request);
        return Ok(res);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync(LogoutRequestDTO request)
    {
        var res = await _authService.LogoutAsync(request);
        return Ok(res);
    }
}