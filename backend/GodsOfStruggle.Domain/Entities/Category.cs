using GodsOfStruggle.Domain.Common;

namespace GodsOfStruggle.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid? ParentCategoryId { get; private set; }
    public string Slug { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;

    public Category? ParentCategory { get; private set; }
    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();
    private readonly List<Category> _subCategories = [];

    private Category() { }

    public static Category Create(string name, string description, string slug, Guid? parentCategoryId = null)
    {


        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("O nome da categoria não pode ser vazio.", nameof(name));
        }

        return new Category
        {
            Name = name.Trim(),
            Description = description,
            Slug = slug,
            ParentCategoryId = parentCategoryId
        };
    }

    public void Update(string name, string description, string slug)
    {
        Name = name;
        Description = description;
        Slug = slug;
        UpdateTimestamp();
    }

    public void Deactivate() { IsActive = false; UpdateTimestamp(); }
    public void Activate() { IsActive = true; UpdateTimestamp(); }
}
