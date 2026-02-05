using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Vendors.Queries.GetAllVendors;

public record GetAllVendorsQuery : IRequest<IEnumerable<Vendor>>;
public class GetAllVendorsQueryHandler : IRequestHandler<GetAllVendorsQuery, IEnumerable<Vendor>>
{
    private readonly IVendorRepository _vendorRepository;

    public GetAllVendorsQueryHandler(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public async Task<IEnumerable<Vendor>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
    {
        return await _vendorRepository.GetAllAsync();
    }
}