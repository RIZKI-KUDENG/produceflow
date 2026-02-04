using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.UseCases.Departments.Commands.CreateDepartment;
using ProduceFlow.Application.UseCases.Departments.Queries.GetAllDepartments;
using ProduceFlow.Application.UseCases.Departments.Queries.GetDepartmentById;
using ProduceFlow.Application.UseCases.Departments.Commands.UpdateDepartment;
using ProduceFlow.Application.UseCases.Departments.Commands.DeleteDepartment;
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
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _mediator.Send(new GetAllDepartmentsQuery());
        return Ok(departments);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetDepartmentByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentRequest request)
    {
        var command = new CreateDepartmentCommand(request.Name, request.CostCenterCode);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentRequest request)
    {
        var command = new UpdateDepartmentCommand(id, request.Name, request.CostCenterCode);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteDepartmentCommand(id));
        return NoContent();
    }
}