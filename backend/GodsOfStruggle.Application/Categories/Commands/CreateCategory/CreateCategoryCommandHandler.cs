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

        await ValidatParentCategoryIdAsync(request.ParentCategoryId, cancellationToken);

        Category category = Category.Create(request.Name, request.Description, request.Slug, request.ParentCategoryId);
        await categoryRepository.AddAsync(category, cancellationToken);
        return ToDto(category);
    }

    private static CategoryDto ToDto(Category c) =>
        new(c.Id, c.Name, c.Description, c.Slug, c.ParentCategoryId, c.IsActive);


    private async Task ValidatParentCategoryIdAsync(Guid? parentCategoryId, CancellationToken cancellationToken)
    {
        if (parentCategoryId.HasValue)
        {
        
            bool parentCategoryExists = await categoryRepository.ExistsAsync(parentCategoryId.Value, cancellationToken);

            if (!parentCategoryExists)
            {
            
                throw new InvalidOperationException("A categoria pai não existe.");
            }
        }
    }
}
