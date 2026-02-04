using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;

namespace ProduceFlow.Application.UseCases.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest<bool>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(request.Id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException($"Category with Id {request.Id} not found.");
        }

        await _categoryRepository.DeleteAsync(request.Id);
        return true;
    }
}