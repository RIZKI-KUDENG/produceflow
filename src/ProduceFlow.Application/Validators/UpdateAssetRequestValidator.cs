using FluentValidation;
using ProduceFlow.Application.DTOs.Assets;

namespace ProduceFlow.Application.Validators;

public class UpdateAssetRequestValidator : AbstractValidator<UpdateAssetRequest>
{
    public UpdateAssetRequestValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Name is required")
        .MaximumLength(100)
        .WithMessage("Name must not exceed 100 characters");
    }
}