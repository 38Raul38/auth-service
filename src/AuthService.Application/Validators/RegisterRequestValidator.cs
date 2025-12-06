using AuthService.Application.Data.DTOs.Request;
using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;


namespace AuthService.Application.Validators;

    public class RegisterRequestDTOValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterRequestDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .Matches(RegexPatterns.Username)
                .MaximumLength(30)
                .WithMessage("Name must be at most 30 characters");
            
            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Surname is required")
                .Matches(RegexPatterns.Username)
                .MaximumLength(30)
                .WithMessage("Surname must be at most 30 characters");
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .Matches(RegexPatterns.Email)
                .WithMessage("Email format is invalid");
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Matches(RegexPatterns.Password)
                .WithMessage("Password format is invalid");
            
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password is required")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }