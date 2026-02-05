using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Locations.Queries.GetLocationById;

public record GetLocationByIdQuery(int Id) : IRequest<Location?>;

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, Location?>
{
    private readonly ILocationRepository _locationRepository;

    public GetLocationByIdQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Location?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _locationRepository.GetByIdAsync(request.Id);
    }
}