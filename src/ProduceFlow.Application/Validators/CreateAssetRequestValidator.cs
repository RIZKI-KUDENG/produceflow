using FluentValidation;
using ProduceFlow.Application.DTOs.Assets;

namespace ProduceFlow.Application.Validators;

public class CreateAssetRequestValidator : AbstractValidator<CreateAssetRequest>
{
    public CreateAssetRequestValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Name is required")
        .MaximumLength(100)
        .WithMessage("Name must not exceed 100 characters");

    }
}