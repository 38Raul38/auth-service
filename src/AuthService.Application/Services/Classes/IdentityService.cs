using System.ComponentModel.DataAnnotations;
using AuthService.Application.Data.DTOs.Request;
using AuthService.Application.Data.DTOs.Response;
using AuthService.Application.Services.Interfaces;
using AuthService.Application.Utils;
using AuthService.Core.Models;
using AuthService.Persistence.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace AuthService.Application.Services.Classes;

public class IdentityService : IAuthService
{
     private readonly IMapper _mapper;
     private readonly UserDbContext _context;
     private readonly IPasswordHasherService _passwordHasherService;
     private readonly TokenManager _tokenManager;
     private readonly EmailSender _emailSender;
     
     public IdentityService(IMapper mapper, UserDbContext context, IPasswordHasherService passwordHasherService, TokenManager tokenManager, EmailSender emailSender)
     {
         _mapper = mapper;
         _context = context;
         _passwordHasherService = passwordHasherService;
         _tokenManager = tokenManager;
         _emailSender = emailSender;
     }

     public async Task<Result> RegisterAsync(RegisterRequestDTO request)
     {
         if (await _context.Users.AnyAsync(u => u.Email == request.Email))
         {
             throw new ValidationException("Email is already in use.");
         }

         var user = _mapper.Map<User>(request);

         user.Password = _passwordHasherService.Hash(request.Password);

         await _context.Users.AddAsync(user);
         await _context.SaveChangesAsync();

         try
         {
             await _emailSender.SendEmailAsync(
                 user.Email,
                 user.Username,
                 $"<h2>Hello, {user.Name}!</h2><p>Welcome to our service!</p>"
             );
         }
         catch
         {
             //????????
         }

         return Result.Success();
     }

     public async Task<TypeResult<AuthResponseDTO>> LoginAsync(LoginRequestDTO request)
     {
         var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

         if (user == null || !_passwordHasherService.Verify(request.Password, user.Password))
         {
             throw new ValidationException("Invalid credentials");
         }

         var roles = await _context.UserRoles
             .Where(ur => ur.UserId == user.Id)
             .Select(ur => ur.Role.Name.ToString())
             .ToListAsync();


         var accessToken = await _tokenManager.CreateTokenAsync(user, roles);
         var refreshToken = await _tokenManager.GenerateRefreshTokenAsync(user, roles);
         var RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

         user.RefreshToken = refreshToken;
         user.RefreshTokenExpiryTime = RefreshTokenExpiryTime;
         await _context.SaveChangesAsync();

         var response = new AuthResponseDTO
         {
             AccessToken = accessToken,
             RefreshToken = refreshToken,
             RefreshTokenExpiresAt = RefreshTokenExpiryTime
         };

         return TypeResult<AuthResponseDTO>.Success(
             message: "Login successful",
             statusCode: 200,
             data: response
         );
     }

     public async Task<TypeResult<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
     {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
    
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new ValidationException("Invalid refresh token");
            }
            
            var roles = await _context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.Name.ToString())
                .ToListAsync();
    
            
            var newAccessToken = await _tokenManager.CreateTokenAsync(user, roles);
            var newRefreshToken = await _tokenManager.GenerateRefreshTokenAsync(user, roles);
            var newRefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = newRefreshTokenExpiryTime;
    
            await _context.SaveChangesAsync();
    
            return TypeResult<RefreshTokenResponse>.Success(
                message: "Token refreshed successfully",
                statusCode: 200,
                data: new RefreshTokenResponse(
                    AccessToken: newAccessToken,
                    RefreshToken: newRefreshToken,
                    RefreshTokenExpiresAt: newRefreshTokenExpiryTime
                )
            );
     }
     
     public async Task<Result> LogoutAsync(LogoutRequestDTO request)
     {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
            
            if (user == null)
            {
                throw new ValidationException("Invalid refresh token");
            }
            
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.MinValue;
            
            await _context.SaveChangesAsync();
            
            return Result.Success("Logout successful");
     }
}