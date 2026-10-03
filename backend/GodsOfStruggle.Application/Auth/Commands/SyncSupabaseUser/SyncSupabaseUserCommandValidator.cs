using FluentValidation;

namespace GodsOfStruggle.Application.Auth.Commands.SyncSupabaseUser;

public class SyncSupabaseUserCommandValidator : AbstractValidator<SyncSupabaseUserCommand>
{
    public SyncSupabaseUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
    }
}
