using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.DTOs.Locations;
using ProduceFlow.Application.UseCases.Locations.Commands.CreateLocation;
using ProduceFlow.Application.UseCases.Locations.Commands.DeleteLocation;
using ProduceFlow.Application.UseCases.Locations.Commands.UpdateLocation;
using ProduceFlow.Application.UseCases.Locations.Queries.GetAllLocations;
using ProduceFlow.Application.UseCases.Locations.Queries.GetLocationById;
using MediatR;

namespace ProduceFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LocationsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public LocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var locations = await _mediator.Send(new GetAllLocationsQuery());
        return Ok(locations);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetLocationByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationRequest request)
    {
        var command = new CreateLocationCommand(request.Name, request.Address);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLocationRequest request)
    {
        var command = new UpdateLocationCommand(id, request.Name, request.Address);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLocationCommand(id));
        return NoContent();
    }
}