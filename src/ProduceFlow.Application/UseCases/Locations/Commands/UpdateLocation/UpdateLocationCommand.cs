using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Locations.Commands.UpdateLocation;

public record UpdateLocationCommand(int LocationId, string Name, string Address) : IRequest<Location>;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Location>
{
    private readonly ILocationRepository _locationRepository;

    public UpdateLocationCommandHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Location> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var existingLocation = await _locationRepository.GetByIdAsync(request.LocationId);
        if (existingLocation == null)
        {
            throw new KeyNotFoundException($"Location with Id {request.LocationId} not found.");
        }
        existingLocation.Name = request.Name;
        existingLocation.Address = request.Address;
        await _locationRepository.UpdateAsync(existingLocation);
        return existingLocation;
    }
}