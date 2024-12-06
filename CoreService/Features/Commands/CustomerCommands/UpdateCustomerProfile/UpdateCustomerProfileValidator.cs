using FluentValidation;

namespace CoreService.Features.Commands.CustomerCommands.UpdateCustomerProfile;

public class UpdateCustomerProfileValidator : AbstractValidator<UpdateCustomerProfileCommand>
{
    public UpdateCustomerProfileValidator()
    {
        RuleFor(command => command)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("command cannot be null or empty.");
        RuleFor(command => command.Payload)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Payload cannot be null or empty.");
        RuleFor(command => command.Payload.DisplayName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("DisplayName cannot be null or empty.");
        RuleFor(command => command.Payload.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("PhoneNumber cannot be null or empty.");
        RuleFor(command => command.Payload.Avatar)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Avatar cannot be null or empty.");
    }
}
