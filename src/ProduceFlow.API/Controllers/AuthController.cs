using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.UseCases.Auth.Commands.Register;
using ProduceFlow.Application.DTOs.Auth;

namespace ProduceFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Authcontroller : ControllerBase
{
    private readonly IMediator _mediator;

    public Authcontroller(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(
            request.FullName,
            request.Email,
            request.Password
        );
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id = userId }, new {message = "User registered successfully"});
    }
}