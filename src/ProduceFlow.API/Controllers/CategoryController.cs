using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.UseCases.Categories.Commands.CreateCategory;
using ProduceFlow.Application.DTOs.Categories;
using MediatR;

namespace ProduceFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand(request.Name, request.DepreciationYears);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}