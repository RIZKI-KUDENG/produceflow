using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using FluentValidation;
using ProduceFlow.Application.DTOs.Assets;


namespace ProduceFlow.Application.UseCases.Assets.Commands.UpdateAsset;

public record UpdateAssetCommand( int Id, string Name, int CategoryId, DateTime PurchaseDate, decimal PurchasePrice, int LocationId, int CurrentHolder, string Status, string SerialNumber) : IRequest<Asset>;

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
            CategoryId = request.CategoryId,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            LocationId = request.LocationId,
            CurrentHolderId = request.CurrentHolder,
            Status = request.Status,
            SerialNumber = request.SerialNumber
       };

       await _validator.ValidateAndThrowAsync(dto, cancellationToken);

       var existingAsset = await _repository.GetByIdAsync(request.Id);

       if (existingAsset is null)
       {
           throw new KeyNotFoundException($"Asset dengan ID {request.Id} tidak ditemukan");
       }

       existingAsset.Name = request.Name;
       existingAsset.PurchaseDate = request.PurchaseDate;
       existingAsset.PurchasePrice = request.PurchasePrice;
       existingAsset.Status = request.Status;
         existingAsset.SerialNumber = request.SerialNumber;
         existingAsset.CategoryId = request.CategoryId;
         existingAsset.LocationId = request.LocationId;
         existingAsset.CurrentHolderId = request.CurrentHolder;
         

       await _repository.UpdateAsync(existingAsset);
       return existingAsset;
    }
}
