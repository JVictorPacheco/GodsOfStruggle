using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using MediatR;

namespace GodsOfStruggle.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = Category.Create(request.Name, request.Description, request.Slug, request.ParentCategoryId);
        await categoryRepository.AddAsync(category, cancellationToken);
        return ToDto(category);
    }

    private static CategoryDto ToDto(Category c) =>
        new(c.Id, c.Name, c.Description, c.Slug, c.ParentCategoryId, c.IsActive);
}
