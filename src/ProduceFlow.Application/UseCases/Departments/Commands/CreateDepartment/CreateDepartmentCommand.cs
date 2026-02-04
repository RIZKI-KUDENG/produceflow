using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using MediatR;

namespace ProduceFlow.Application.UseCases.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand(string Name, string CostCenterCode) : IRequest<Department>;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Department>
{
    private readonly IDepartmentRepository _departmentRepository;
    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Department> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department
        {
            Name = request.Name,
            CostCenterCode = request.CostCenterCode
        };
        return await _departmentRepository.AddAsync(department);
    }
}
