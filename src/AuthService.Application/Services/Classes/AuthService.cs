using AuthService.Application.Services.Interfaces;
using AuthService.Persistance.Context;

namespace AuthService.Application.Services.Classes;

public class AuthService : IAuthService
{
     private readonly UserDbContext _context;
}