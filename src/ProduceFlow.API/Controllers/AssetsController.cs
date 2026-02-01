using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.DTOs.Assets;
using ProduceFlow.Application.Services;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.UseCases.Assets.Commands.CreateAsset;
using ProduceFlow.Application.UseCases.Assets.Commands.DeleteAsset;
using ProduceFlow.Application.UseCases.Assets.Commands.UpdateAsset;
using ProduceFlow.Application.UseCases.Assets.Queries.GetAllAssets;
using ProduceFlow.Application.UseCases.Assets.Queries.GetAssetById;
using MediatR;

namespace ProduceFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assets = await _mediator.Send(new GetAllAssetsQuery());
        return Ok(assets);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssetRequest request)
    {
        var command = new CreateAssetCommand(
            request.Name,
            request.Description,
            request.Price,
            request.Quantity,
            request.Status
        );
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetAssetByIdQuery(id));
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteAssetCommand(id));
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAssetRequest request)
    {
        await _mediator.Send(new UpdateAssetCommand(id, request.Name, request.Description, request.Price, request.Quantity, request.Status));
        return NoContent();
    }
}