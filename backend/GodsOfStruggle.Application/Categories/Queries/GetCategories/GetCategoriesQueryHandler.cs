using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using MediatR;

namespace GodsOfStruggle.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        List<Category> categories = await categoryRepository.GetAllActiveAsync(cancellationToken);
        return categories.Select(c => new CategoryDto(c.Id, c.Name, c.Description, c.Slug, c.ParentCategoryId, c.IsActive)).ToList();
    }
}
