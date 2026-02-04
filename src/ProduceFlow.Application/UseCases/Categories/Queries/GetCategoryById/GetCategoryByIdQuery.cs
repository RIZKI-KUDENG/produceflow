using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(int Id) : IRequest<Category?>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetByIdAsync(request.Id);
    }
}