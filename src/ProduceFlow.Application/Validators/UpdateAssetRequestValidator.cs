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

        RuleFor(x => x.Price)
        .GreaterThan(0)
        .WithMessage("Price must be greater than 0");

        RuleFor(x => x.Quantity)
        .GreaterThanOrEqualTo(0)
        .WithMessage("Quantity must be greater than or equal to 0");

        RuleFor(x => x.Status)
        .IsInEnum()
        .WithMessage("Status is invalid");
    }
}