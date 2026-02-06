using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.DTOs.ApprovalLogs;
using ProduceFlow.Application.UseCases.ApprovalLogs.Commands.CreateApprovalLog;
using ProduceFlow.Application.UseCases.ApprovalLogs.Queries.GetAllApprovalLogs;
using ProduceFlow.Application.UseCases.ApprovalLogs.Queries.GetApprovalLogById;
using MediatR;
using ProduceFlow.Application.UseCases.ApprovalLogs.Commands.DeleteApprovalLog;
using ProduceFlow.Application.UseCases.ApprovalLogs.Commands.UpdateApprovalLog;

namespace ProduceFlow.API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class ApprovalLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApprovalLogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllApprovalLogs()
    {
        var query = new GetAllApprovalLogsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApprovalLogById(int id)
    {
        var query = new GetApprovalLogByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateApprovalLog([FromBody] CreateApprovalLogRequest request)
    {
        var command = new CreateApprovalLogCommand(
            request.PurchaseRequestId,
            request.ApprovedById,
            request.Action,
            request.Comments,
            request.ActionDate
        );
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetApprovalLogById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateApprovalLog(int id, [FromBody] UpdateApprovalLogRequest request)
    {
        var command = new UpdateApprovalLogCommand(id,  request.Action, request.Comments);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteApprovalLog(int id)
    {
        var command = new DeleteApprovalLogCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}