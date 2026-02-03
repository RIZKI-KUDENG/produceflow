using Moq;
using ProduceFlow.Application.UseCases.Categories.Commands.CreateCategory;
using ProduceFlow.Application.DTOs.Categories;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;
using Xunit;
using FluentAssertions;
using FluentValidation;
using ProduceFlow.Application.Validators;

namespace ProduceFlow.UnitTests;

public class CategoryCommandTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly CreateCategoryCommandHandler _handler;

    public CategoryCommandTests()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _handler = new CreateCategoryCommandHandler(_categoryRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_CreateCategory()
    {
        //arange
        var command = new CreateCategoryCommand("Test Category", 5);

        //act
        var result = await _handler.Handle(command, CancellationToken.None);

        //assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Test Category");
        result.DepreciationYears.Should().Be(5);

        _categoryRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Category>()), Times.Once);

        
    }
}