using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Vendors.Queries.GetVendorById;

public record GetVendorByIdQuery(int Id) : IRequest<Vendor?>;

public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, Vendor?>
{
    private readonly IVendorRepository _vendorRepository;

    public GetVendorByIdQueryHandler(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public async Task<Vendor?> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        return await _vendorRepository.GetByIdAsync(request.Id);
    }
}