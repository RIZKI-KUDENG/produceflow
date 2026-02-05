using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Vendors.Commands.CreateVendor;

public record CreateVendorCommand(string Name, string ContactPerson, string Phone, string Address, bool IsVerified) : IRequest<Vendor>;

public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, Vendor>
{
    private readonly IVendorRepository _vendorRepository;
    
    public CreateVendorCommandHandler(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public async Task<Vendor> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        var vendor = new Vendor
        {
            Name = request.Name,
            ContactPerson = request.ContactPerson,
            Phone = request.Phone,
            Address = request.Address,
            IsVerified = request.IsVerified
        };

        return await _vendorRepository.AddAsync(vendor);
    }
}