using FluentValidation;
using Moq;
using ProduceFlow.Application.DTOs.Auth;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Application.UseCases.Auth.Commands.Register;
using ProduceFlow.Application.Validators; 
using ProduceFlow.Domain.Entities;
using Xunit;
using FluentAssertions;

namespace ProduceFlow.UnitTests;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly Mock<IPasswordHasher> _mockHasher;
    private readonly IValidator<RegisterUserRequest> _validator; 
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _mockHasher = new Mock<IPasswordHasher>();
        
        _validator = new RegisterUserRequestValidator();

        _handler = new RegisterUserCommandHandler(
            _mockRepo.Object, 
            _validator, 
            _mockHasher.Object
        );
    }

    [Fact]
    public async Task Handle_Should_ReturnUser_When_InputIsValid()
    {
        // === ARRANGE ===
        var command = new RegisterUserCommand("Rizki", "rizki@test.com", "password123");
        
        // Setup Hasher & Repo
        _mockHasher.Setup(h => h.Hash("password123")).Returns("hashed_secret_123");
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask); 

        // === ACT ===
        var result = await _handler.Handle(command, CancellationToken.None);

        // === ASSERT ===
        result.Should().NotBeNull();
        result.Email.Should().Be("rizki@test.com");
        result.PasswordHash.Should().Be("hashed_secret_123");
        
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ThrowValidationException_When_InputIsInvalid()
    {
        // === ARRANGE ===
        var command = new RegisterUserCommand("Rizki", "bukan email", ""); 

        // === ACT & ASSERT ===
        await Assert.ThrowsAsync<ValidationException>(() => 
            _handler.Handle(command, CancellationToken.None)
        );

        _mockRepo.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
    }
}