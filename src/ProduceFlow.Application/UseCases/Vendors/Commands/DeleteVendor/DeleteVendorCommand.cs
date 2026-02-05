using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Vendors.Commands.DeleteVendor;

public record DeleteVendorCommand(int Id) : IRequest<bool>;

public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, bool>
{
    private readonly IVendorRepository _vendorRepository;

    public DeleteVendorCommandHandler(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public async Task<bool> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {
        var existingVendor = await _vendorRepository.GetByIdAsync(request.Id);
        if (existingVendor == null)
        {
            return false; 
        }

        await _vendorRepository.DeleteAsync(request.Id);
        return true; 
    }
}