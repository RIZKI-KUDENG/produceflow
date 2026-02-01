using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using FluentValidation;
using ProduceFlow.Application.DTOs.Assets;


namespace ProduceFlow.Application.UseCases.Assets.Commands.UpdateAsset;

public record UpdateAssetCommand(int Id, string Name, string? Description, decimal Price, int Quantity, AssetStatus Status) : IRequest<Asset>;

public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, Asset>
{
    private readonly IAssetRepository _repository;
    private readonly IValidator<UpdateAssetRequest> _validator;

    public UpdateAssetCommandHandler(IAssetRepository repository, IValidator<UpdateAssetRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Asset> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
       var dto = new UpdateAssetRequest
       {
           Name = request.Name,
           Description = request.Description,
           Price = request.Price,
           Quantity = request.Quantity,
           Status = request.Status
       };

       await _validator.ValidateAndThrowAsync(dto, cancellationToken);

       var existingAsset = await _repository.GetByIdAsync(request.Id);

       if (existingAsset is null)
       {
           throw new KeyNotFoundException($"Asset dengan ID {request.Id} tidak ditemukan");
       }

       existingAsset.Name = request.Name;
       existingAsset.Description = request.Description;
       existingAsset.Price = request.Price;
       existingAsset.Quantity = request.Quantity;
       existingAsset.Status = request.Status;
       existingAsset.UpdatedAt = DateTime.UtcNow;

       await _repository.UpdateAsync(existingAsset);
       return existingAsset;
    }
}
