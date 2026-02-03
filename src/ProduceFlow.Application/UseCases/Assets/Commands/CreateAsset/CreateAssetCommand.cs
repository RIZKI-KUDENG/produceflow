using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using FluentValidation;
using ProduceFlow.Application.DTOs.Assets;

namespace ProduceFlow.Application.UseCases.Assets.Commands.CreateAsset;

public record CreateAssetCommand(string Name, int CategoryId, DateTime PurchaseDate, decimal PurchasePrice, int LocationId, int CurrentHolder, string Status, string SerialNumber) : IRequest<Asset>;

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
            CategoryId = request.CategoryId,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            LocationId = request.LocationId,
            CurrentHolder = request.CurrentHolder,
            Status = request.Status,
            SerialNumber = request.SerialNumber
        };
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);

        var newAsset = new Asset
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            LocationId = request.LocationId,
            CurrentHolder = request.CurrentHolder,
            Status = request.Status,
            SerialNumber = request.SerialNumber
        };

        return await _repository.AddAsync(newAsset);
    }
}