using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using GodsOfStruggle.Domain.Exceptions;
using GodsOfStruggle.Domain.ValueObjects;
using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<UpdateProductCommand, Guid>
{
    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Produto {request.Id} não encontrado.");

        product.Update(request.Name, request.Description, new Money(request.BasePrice), request.CategoryId);
        await productRepository.UpdateAsync(product, cancellationToken);
        return product.Id;
    }
}
