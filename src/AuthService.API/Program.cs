using AuthService.Application.Services.Classes;
using AuthService.Application.Services.Interfaces;
using AuthService.Application.Utils;
using AuthService.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using static Microsoft.AspNetCore.Builder.WebApplication;

var builder = CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// OpenAPI / Scalar
builder.Services.AddOpenApi();

// DbContext
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("AuthServiceDbConnection")
    )
);

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DI
builder.Services.AddScoped<EmailSender>();
builder.Services.AddScoped<TokenManager>();
builder.Services.AddScoped<IAuthService, IdentityService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

// ======================
// CORS (FIXED)
// ======================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173") // порт фронта
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ======================
// AUTH
// ======================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5),
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration["JWT:SecretKey"]
            )
        )
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// ======================
// MIDDLEWARE ORDER (CRITICAL)
// ======================
app.UseCors("AllowReactApp");   // ← СТРОГО ПЕРВЫМ

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// OpenAPI / Scalar
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/", () => "Hello World!");

app.Run();