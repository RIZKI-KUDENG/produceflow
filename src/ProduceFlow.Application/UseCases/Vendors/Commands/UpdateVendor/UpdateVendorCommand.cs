using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Vendors.Commands.UpdateVendor;

public record UpdateVendorCommand(int Id, string Name, string ContactPerson, string Phone, string Address,  bool IsVerified) : IRequest<Vendor>;

public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, Vendor>
{
    private readonly IVendorRepository _vendorRepository;

    public UpdateVendorCommandHandler(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public async Task<Vendor> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        var existingVendor = await _vendorRepository.GetByIdAsync(request.Id);
        if (existingVendor == null)
        {
            throw new KeyNotFoundException($"Vendor dengan ID {request.Id} tidak ditemukan");
        }

        existingVendor.Name = request.Name;
        existingVendor.ContactPerson = request.ContactPerson;
        existingVendor.Phone = request.Phone;
        existingVendor.Address = request.Address;
        existingVendor.IsVerified = request.IsVerified;

        await _vendorRepository.UpdateAsync(existingVendor);
        return existingVendor;
    }
}