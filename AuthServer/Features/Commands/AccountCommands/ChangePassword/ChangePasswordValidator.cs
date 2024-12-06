using FluentValidation;

namespace AuthServer.Features.Commands.AccountCommands.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(command => command.Payload)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Payload cannot be null or empty.");
        RuleFor(command => command.Payload.NewPassword)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("New password cannot be null or empty.");
        RuleFor(command => command.Payload.OldPassword)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Old password cannot be null or empty.");
    }
}
