

namespace ProduceFlow.Application.DTOs.Departments;

public class CreateDepartmentRequest
{
    public string Name { get; set; } = string.Empty;
    public string CostCenterCode { get; set; } = string.Empty;
}