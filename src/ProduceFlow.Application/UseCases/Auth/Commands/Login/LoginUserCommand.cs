using MediatR;
using FluentValidation;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Application.DTOs.Auth;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Auth.Commands.Login;

public record LoginUserCommand(string Email, string Password) : IRequest<AuthenticationResponse>;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthenticationResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if(user == null || !_passwordHasher.Verify(user.PasswordHash, request.Password))
        {
            throw new UnauthorizedAccessException("Email atau password salah");
        }

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        refreshToken.UserId = user.Id;

        await _userRepository.UpdateAsync(user);

        return new AuthenticationResponse
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }
}