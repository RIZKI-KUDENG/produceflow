using FluentValidation;
using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Application.DTOs.Auth;

namespace ProduceFlow.Application.UseCases.Auth.Commands.Register;

public record RegisterUserCommand(string FullName, string Email, string Password, int DepartementId) : IRequest<User>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IValidator<RegisterUserRequest> _validator;
    private readonly IRoleRepository _roleRepository;

    public RegisterUserCommandHandler(IUserRepository repository, IValidator<RegisterUserRequest> validator, IPasswordHasher passwordHasher, IRoleRepository roleRepository)
    {
        _repository = repository;
        _validator = validator;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
    }

    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var dto = new RegisterUserRequest
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password,
            DepartementId = request.DepartementId
        };
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);
        var existingEmail = await _repository.GetByEmailAsync(request.Email);
        if (existingEmail != null)
        {
            throw new ArgumentException("Email is already registered.");
        }

        const string defaultRoleName = "Staff";
        var staffRole = await _roleRepository.GetByNameAsync(defaultRoleName);
        if (staffRole == null)
        {
            throw new Exception($"System Error: Default role '{defaultRoleName}'");
        }

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            IsActive = true,
            DepartementId = request.DepartementId
        };
        var role = new UserRole
        {
            Role = staffRole,
            User = user
        };
        user.UserRoles.Add(role);

        await _repository.AddAsync(user);

        return user;
    }
}