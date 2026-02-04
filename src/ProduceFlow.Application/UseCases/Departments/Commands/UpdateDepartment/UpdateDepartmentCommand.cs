using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Departments.Commands.UpdateDepartment;

public record UpdateDepartmentCommand(int Id, string Name, string CostCenterCode) : IRequest<Department>;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Department>
{
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Department> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var existingDepartment = await _departmentRepository.GetByIdAsync(request.Id);
        if (existingDepartment == null)
        {
            throw new KeyNotFoundException($"Department with Id {request.Id} not found.");
        }
        existingDepartment.Name = request.Name;
        existingDepartment.CostCenterCode = request.CostCenterCode;
         await _departmentRepository.UpdateAsync(existingDepartment);
        return existingDepartment;
    }
}