using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using GodsOfStruggle.Domain.ValueObjects;
using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = Product.Create(request.Name, request.Description, new Money(request.BasePrice), request.CategoryId);
        await productRepository.AddAsync(product, cancellationToken);
        return product.Id;
    }
}
