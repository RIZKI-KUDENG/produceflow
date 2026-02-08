using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.UseCases.Users.Queries.GetUser;


namespace ProduceFlow.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string? search)
    {
        var query = new GetUsersQuery(search);
        var users = await _mediator.Send(query);
        return Ok(users);
    }
}