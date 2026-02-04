using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(int Id, string Name, int DepreciationYears) : IRequest<Category>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(request.Id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException($"Category with Id {request.Id} not found.");
        }
        existingCategory.Name = request.Name;
        existingCategory.DepreciationYears = request.DepreciationYears;
         await _categoryRepository.UpdateAsync(existingCategory);
        return existingCategory;
    }
}