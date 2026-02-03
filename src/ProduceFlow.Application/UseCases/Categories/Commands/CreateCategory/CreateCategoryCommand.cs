using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;



namespace ProduceFlow.Application.UseCases.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, int DepreciationYears) : IRequest<Category>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

   public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            DepreciationYears = request.DepreciationYears
        };

        return await _categoryRepository.AddAsync(category);
    }
}
