using Microsoft.AspNetCore.Mvc;
using MediatR;
using ProduceFlow.Application.DTOs.Vendors;
using ProduceFlow.Application.UseCases.Vendors.Commands.CreateVendor;
using ProduceFlow.Application.UseCases.Vendors.Commands.DeleteVendor;
using ProduceFlow.Application.UseCases.Vendors.Commands.UpdateVendor;
using ProduceFlow.Application.UseCases.Vendors.Queries.GetVendorById;
using ProduceFlow.Application.UseCases.Vendors.Queries.GetAllVendors;



namespace ProduceFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VendorsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public VendorsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllVendorsQuery();
        var vendors = await _mediator.Send(query);
        return Ok(vendors);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetVendorByIdQuery(id);
        var vendor = await _mediator.Send(query);
        if (vendor == null)
        {
            return NotFound();
        }
        return Ok(vendor);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVendorRequest request)
    {
        var command = new CreateVendorCommand(request.Name, request.ContactPerson, request.Phone, request.Address, request.IsVerified);
        var vendor = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = vendor.Id }, vendor);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVendorRequest request)
    {
        var command = new UpdateVendorCommand(id, request.Name, request.ContactPerson, request.Phone, request.Address, request.IsVerified);
        var updatedVendor = await _mediator.Send(command);
        if (updatedVendor == null)
        {
            return NotFound();
        }
        return Ok(updatedVendor);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteVendorCommand(id);
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}