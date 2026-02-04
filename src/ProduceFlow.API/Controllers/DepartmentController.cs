using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.UseCases.Departments.Commands.CreateDepartment;
using ProduceFlow.Application.DTOs.Departments;
using MediatR;

namespace ProduceFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentRequest request)
    {
        var command = new CreateDepartmentCommand(request.Name, request.CostCenterCode);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}