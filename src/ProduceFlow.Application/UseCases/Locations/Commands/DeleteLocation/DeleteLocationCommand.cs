using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Locations.Commands.DeleteLocation;

public record DeleteLocationCommand(int LocationId) : IRequest<bool>;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
{
    private readonly ILocationRepository _locationRepository;

    public DeleteLocationCommandHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetByIdAsync(request.LocationId);
        if (location == null)
        {
            throw new KeyNotFoundException($"Location with Id {request.LocationId} not found.");
        }

        await _locationRepository.DeleteAsync(request.LocationId);
        return true;
    }
}