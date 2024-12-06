using FluentValidation;

namespace AuthServer.Features.Commands.AccountCommands.ConfirmVerificationEmail;

public class ConfirmVerificationEmailValidator : AbstractValidator<ConfirmVerificationEmailCommand>
{
    public ConfirmVerificationEmailValidator()
    {
        RuleFor(command => command.Payload)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Payload cannot be null or empty.");
        RuleFor(command => command.Payload.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email cannot be null or empty.");
        RuleFor(command => command.Payload.VerificationToken)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("VerificationToken cannot be null or empty.");
    }
}
