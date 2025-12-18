using AuthService.Application.Data.DTOs.Request;
using AuthService.Application.Data.DTOs.Response;
using AuthService.Application.Services.Interfaces;
using AuthService.Persistence.Context;
// using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Application.Services.Classes;

public class AccountService : IAccountService
{
    private readonly UserDbContext _context;
    private readonly IPasswordHasherService _passwordHasherService;

    
    public AccountService(UserDbContext context, PasswordHasherService passwordHasherService)
    {
        _context = context;
        _passwordHasherService = passwordHasherService;
    }
    
    public async Task<Result> ChangePasswordAsync(string userIdString, ChangePasswordRequest.ChangePasswordRequestDTO request)
    {
        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Result.Error("Invalid User ID format");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return Result.Error("User not found");

        if (!_passwordHasherService.Verify(request.CurrentPassword, user.Password))
            return Result.Error("Invalid current password");
    
        if (request.NewPassword != request.ConfirmNewPassword)
            return Result.Error("New password and confirmation do not match");
        
        if (request.NewPassword == request.CurrentPassword)
            return Result.Error("New password cannot be the same as the old one");

        var newPasswordHash = _passwordHasherService.Hash(request.NewPassword);

        user.Password = newPasswordHash;
    
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> ChangeEmailAsync(string userIdString, ChangeEmailRequest.ChangeEmailRequestDTO request)
    {
        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Result.Error("Invalid user ID format");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return Result.Error("User not found");
        }

        if (!_passwordHasherService.Verify(request.Password, user.Password))
        {
            return Result.Error("Invalid password");
        }

        var emailExists = await _context.Users.AnyAsync(u => u.Email == request.NewEmail && u.Id != userId);
        if (emailExists)
        {
            return Result.Error("Email is already in use");
        }
    
        if (user.Email == request.NewEmail)
        {
            return Result.Error("New email is the same as the current email");
        }

        user.Email = request.NewEmail;
    

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        // await _emailService.SendConfirmationEmailAsync(user.Email, ...);

        return Result.Success();
    }
}