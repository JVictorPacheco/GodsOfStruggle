using FluentValidation;
using MediatR;

namespace GodsOfStruggle.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next(cancellationToken);

        ValidationContext<TRequest> context = new(request);

        IEnumerable<FluentValidation.Results.ValidationFailure> failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null);

        if (failures.Any())
            throw new ValidationException(failures);

        return await next(cancellationToken);
    }
}
