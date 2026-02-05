using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.UseCases.Auth.Commands.Register;
using ProduceFlow.Application.UseCases.Auth.Commands.Login;
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
            request.Password,
            request.DepartmentId
        );
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id = userId }, new {message = "User registered successfully"});
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var command = new LoginUserCommand(request.Email, request.Password);
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}