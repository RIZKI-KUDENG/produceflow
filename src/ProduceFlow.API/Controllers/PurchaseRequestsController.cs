using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.DTOs.PurchaseRequests;
using ProduceFlow.Application.UseCases.PurchaseRequests.Commands.UpdatePurchaseRequest;
using ProduceFlow.Application.UseCases.PurchaseRequests.Commands.CreatePurchaseRequest;
using ProduceFlow.Application.UseCases.PurchaseRequests.Commands.DeletePurchaseRequest;
using ProduceFlow.Application.UseCases.PurchaseRequests.Queries.GetAllPurchaseRequests;
using ProduceFlow.Application.UseCases.PurchaseRequests.Queries.GetPurchaseRequestById;
using MediatR;


namespace ProduceFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseRequestsController : ControllerBase
{
    private readonly IMediator _mediator;
    public PurchaseRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllPurchaseRequestsQuery();
        var purchaseRequests = await _mediator.Send(query);
        return Ok(purchaseRequests);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetPurchaseRequestByIdQuery(id);
        var purchaseRequest = await _mediator.Send(query);
        if (purchaseRequest == null)
        {
            return NotFound();
        }
        return Ok(purchaseRequest);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseRequest request)
    {
       var command = new CreatePurchaseRequestCommand(
            request.RequestNumber,
            request.RequesterId,
            request.RequestDate,
            request.TotalEstimatedCost,
            request.Status,
            request.Reason
        );
        var purchaseRequest = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = purchaseRequest.Id }, purchaseRequest);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePurchaseRequest request)
    {
        var command = new UpdatePurchaseRequestCommand(
            id,
            request.TotalEstimatedCost,
            request.Status,
            request.Reason
        );
        var updatedPurchaseRequest = await _mediator.Send(command);
        return Ok(updatedPurchaseRequest);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeletePurchaseRequestCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}