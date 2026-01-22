using System.ComponentModel.DataAnnotations;
using AuthService.Application.Data.DTOs.Response;
using AuthService.Application.Services.Interfaces;
using AuthService.Application.Utils;
using AuthService.Core.Models;
using AuthService.Persistence.Context;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthService.Application.Services.Classes;

public sealed class GoogleAuthService : IGoogleAuthService
{
    private readonly UserDbContext _context;
    private readonly TokenManager _tokenManager;
    private readonly IConfiguration _configuration;

    public GoogleAuthService(IConfiguration configuration, UserDbContext context, TokenManager tokenManager)
    {
        _configuration = configuration;
        _context = context;
        _tokenManager = tokenManager;
    }

    public async Task<AuthResponseDTO> GoogleLoginAsync(string credential)
    {
        if (string.IsNullOrWhiteSpace(credential) || credential.Split('.').Length != 3)
            throw new ValidationException("Invalid google credential token");

        var clientId = _configuration["Google:ClientId"];
        if (string.IsNullOrWhiteSpace(clientId))
            throw new InvalidOperationException("Google ClientId is not configured");

        GoogleJsonWebSignature.Payload payload;

        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(
                credential,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                }
            );
        }
        catch (InvalidJwtException)
        {
            throw new ValidationException("Invalid google credential token");
        }

        var email = payload.Email;
        if (string.IsNullOrWhiteSpace(email))
            throw new ValidationException("Google account email is missing");

        var fullName = payload.Name;

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            user = new User
            {
                Email = email,
                FullName = fullName ?? email,
                IsConfirmed = true
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        var roles = await _context.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Select(ur => ur.Role.Name.ToString())
            .ToListAsync();

        var accessToken = await _tokenManager.CreateTokenAsync(user, roles);
        var refreshToken = await _tokenManager.GenerateRefreshTokenAsync(user, roles);
        var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
        await _context.SaveChangesAsync();

        return new AuthResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = refreshTokenExpiryTime
        };
    }
}
