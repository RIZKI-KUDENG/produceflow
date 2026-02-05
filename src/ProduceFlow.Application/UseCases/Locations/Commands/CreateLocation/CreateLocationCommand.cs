using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;
using MediatR;

namespace ProduceFlow.Application.UseCases.Locations.Commands.CreateLocation;

public record CreateLocationCommand(string Name, string Address) : IRequest<Location>;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Location>
{
    private readonly ILocationRepository _locationRepository;

    public CreateLocationCommandHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Location> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = new Location
        {
            Name = request.Name,
            Address = request.Address
        };

        await _locationRepository.AddAsync(location);

        return location;
    }
}