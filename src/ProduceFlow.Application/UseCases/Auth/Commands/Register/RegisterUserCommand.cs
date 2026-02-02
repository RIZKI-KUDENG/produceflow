using FluentValidation;
using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Application.DTOs.Auth;

namespace ProduceFlow.Application.UseCases.Auth.Commands.Register;

public record RegisterUserCommand(string FullName, string Email, string Password) : IRequest<User>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IValidator<RegisterUserRequest> _validator;

    public RegisterUserCommandHandler(IUserRepository repository, IValidator<RegisterUserRequest> validator, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var dto = new RegisterUserRequest
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password
        };
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            IsActive = true 
        };

        return await _repository.AddAsync(user);
    }
}