using System.ComponentModel.DataAnnotations;
using AuthService.Application.Data.DTOs.Request;
using AuthService.Application.Data.DTOs.Response;
using AuthService.Application.Services.Interfaces;
using AuthService.Application.Utils;
using AuthService.Core.Models;
using AuthService.Persistence.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Result = FluentResults.Result;
using Res = AuthService.Application.Data.DTOs.Response.Result;
namespace AuthService.Application.Services.Classes;

public class AuthService : IAuthService
{
     private readonly IMapper _mapper;
     private readonly UserDbContext _context;
     private readonly IPasswordHasherService _passwordHasherService;
     private readonly TokenManager _tokenManager;
     private readonly EmailSender _emailSender;
     
     public AuthService(IMapper mapper, UserDbContext context, IPasswordHasherService passwordHasherService, TokenManager tokenManager, EmailSender emailSender)
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
         }

         return Res.Success();
     }

     public async Task<TypeResult<AuthResponseDTO>> LoginAsync(LoginRequestDTO request)
     {
         var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

         if (user == null || !_passwordHasherService.Verify(request.Password, user.Password))
         {
             throw new ValidationException("Invalid credentials");
         }
         
         
     }
     
     public async Task<TypeResult<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
     {
         
     }
     
     public async Task<Result> LogoutAsync(LogoutRequestDTO request)
     {
         
     }
}