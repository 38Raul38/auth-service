using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;

namespace AuthService.Application.Validators;

public class LoginRequestDTOValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestDTOValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("Email is required");

        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}