using AuthService.Application.Data.DTOs.Response;
using AuthService.Application.Services.Interfaces;
using AuthService.Application.Utils;
using AuthService.Core.Models;
using AuthService.Persistence.Context;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthService.Application.Services.Classes;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly UserDbContext _context;
    private readonly TokenManager _tokenManager;
    private readonly IConfiguration _configuration;

    public GoogleAuthService(
        IConfiguration configuration, UserDbContext context, TokenManager tokenManager)
    {
        _configuration = configuration;
        _context = context;
        _tokenManager = tokenManager;
    }

    public async Task<AuthResponseDTO> GoogleLoginAsync(string credential)
    {
        // 1. Валидация Google JWT token
        var validPayload = await GoogleJsonWebSignature.ValidateAsync(
            credential,
            new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _configuration["Google:ClientId"] }
            }
        );

        // 2. Извлечение данных из Google token
        var email = validPayload.Email;
        var fullName = validPayload.Name;
        var googleId = validPayload.Subject;
        var pictureUrl = validPayload.Picture;

        // 3. Найти или создать пользователя
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            // Создать нового пользователя
            user = new User
            {
                Email = email,
                FullName = fullName,
                IsConfirmed = true,
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        var roles = await _context.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Select(ur => ur.Role.Name.ToString())
            .ToListAsync();

        // 4. Создать JWT token
        var accessToken = await _tokenManager.CreateTokenAsync(user, roles);
        var refreshToken = await _tokenManager.GenerateRefreshTokenAsync(user, roles);
        var RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        // 5. Вернуть ответ
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = RefreshTokenExpiryTime;
        await _context.SaveChangesAsync();

        return new AuthResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = RefreshTokenExpiryTime
        };
    }
}