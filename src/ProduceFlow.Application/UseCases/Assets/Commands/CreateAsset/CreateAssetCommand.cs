using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using FluentValidation;
using ProduceFlow.Application.DTOs.Assets;

namespace ProduceFlow.Application.UseCases.Assets.Commands.CreateAsset;

public record CreateAssetCommand(string Name, string Description, decimal Price, int Quantity, AssetStatus Status) : IRequest<Asset>;

public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Asset>
{
    private readonly IAssetRepository _repository;
    private readonly IValidator<CreateAssetRequest> _validator;

    public CreateAssetCommandHandler(IAssetRepository repository, IValidator<CreateAssetRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Asset> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateAssetRequest
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
            Status = request.Status
        };
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);

        var newAsset = new Asset
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
            Status = request.Status
        };

        return await _repository.AddAsync(newAsset);
    }
}