using System.Data;
using FluentValidation;
using ProduceFlow.Application.DTOs.Auth;

namespace ProduceFlow.Application.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Email)
        .EmailAddress()
        .WithMessage("Email is invalid");

        RuleFor(x => x.Password)
        .MinimumLength(6)
        .WithMessage("Password must be at least 6 characters long");
    }
}