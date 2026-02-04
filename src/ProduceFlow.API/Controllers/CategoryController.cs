using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.UseCases.Categories.Commands.CreateCategory;
using ProduceFlow.Application.UseCases.Categories.Queries.GetAllCategories;
using ProduceFlow.Application.UseCases.Categories.Queries.GetCategoryById;
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


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand(request.Name, request.DepreciationYears);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}