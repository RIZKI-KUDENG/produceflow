using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Locations.Queries.GetAllLocations;

public record GetAllLocationsQuery : IRequest<IEnumerable<Location>>;

public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, IEnumerable<Location>>
{
    private readonly ILocationRepository _locationRepository;

    public GetAllLocationsQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<IEnumerable<Location>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        return await _locationRepository.GetAllAsync();
    }
}