using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.UseCases.PurchaseRequestItems.Queries.GetAllPurchaseRequestItems;
using ProduceFlow.Application.UseCases.PurchaseRequestItems.Queries.GetPurchaseRequestItemById;
using ProduceFlow.Application.UseCases.PurchaseRequestItems.Commands.CreatePurchaseRequestItem;
using ProduceFlow.Application.UseCases.PurchaseRequestItems.Commands.UpdatePurchaseRequestItem;
using ProduceFlow.Application.UseCases.PurchaseRequestItems.Commands.DeletePurchaseRequestItem;
using ProduceFlow.Application.DTOs.PurchaseRequestItems;

namespace ProduceFlow.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PurchaseRequestItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public PurchaseRequestItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllPurchaseRequestItemsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetPurchaseRequestItemByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseRequestItemCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePurchaseRequestItem updateRequest)
    {
        var command = new UpdatePurchaseRequestItemCommand(
            id,
            updateRequest.ItemName,
            updateRequest.Specifications,
            updateRequest.Quantity,
            updateRequest.EstimatedCost
        );
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeletePurchaseRequestItemCommand(id);
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
