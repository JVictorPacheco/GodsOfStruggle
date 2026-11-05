using FluentValidation;

namespace GodsOfStruggle.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
	public CreateOrderCommandValidator()
	{
        RuleFor(x => x.OrderItems).NotEmpty().WithMessage("Itens do pedido são obrigatórios");
                
        RuleForEach(x => x.OrderItems).ChildRules(orderItem =>
        {
            orderItem.RuleFor(x => x.ProductVariantId).NotEmpty().WithMessage("ID da variante do produto é obrigatório");
            orderItem.RuleFor(x => x.Quantity).InclusiveBetween(1, 99).WithMessage("Quantidade deve ser entre 1 e 99");
        });

        RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage("Endereço de envio é obrigatório");

        RuleFor(x => x.ShippingAddress).ChildRules(address =>
        {
            address.RuleFor(x => x.Street).NotEmpty().WithMessage("Rua é obrigatória").MaximumLength(200).WithMessage("Rua deve ter no máximo 200 caracteres");
            address.RuleFor(x => x.Number).NotEmpty().WithMessage("Número é obrigatório").MaximumLength(10).WithMessage("Número deve ter no máximo 10 caracteres");
            address.RuleFor(x => x.Neighborhood).NotEmpty().WithMessage("Bairro é obrigatório").MaximumLength(100).WithMessage("Bairro deve ter no máximo 100 caracteres");
            address.RuleFor(x => x.City).NotEmpty().WithMessage("Cidade é obrigatória").MaximumLength(100).WithMessage("Cidade deve ter no máximo 100 caracteres");
            address.RuleFor(x => x.State).NotEmpty().WithMessage("Estado é obrigatório").Matches(@"^[A-Z]{2}$").WithMessage("Estado deve ser a sigla de 2 letras");
            address.RuleFor(x => x.ZipCode).NotEmpty().WithMessage("CEP é obrigatório").Matches(@"^\d{5}-\d{3}$").WithMessage("CEP inválido");
            address.RuleFor(x => x.Complement).MaximumLength(100).WithMessage("Complemento deve ter no máximo 100 caracteres");
        });

        RuleFor(x => x.PaymentMethod).IsInEnum().WithMessage("Método de pagamento inválido");
    }
}
