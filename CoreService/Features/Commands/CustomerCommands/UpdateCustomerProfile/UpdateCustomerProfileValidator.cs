using FluentValidation;
using Shared.Constants;

namespace CoreService.Features.Commands.CustomerCommands.UpdateCustomerProfile;

public class UpdateCustomerProfileValidator : AbstractValidator<UpdateCustomerProfileCommand>
{
    public UpdateCustomerProfileValidator()
    {
        RuleFor(command => command)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("command is required");
        
        RuleFor(command => command.Payload)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Payload is required");
        
        RuleFor(command => command.Payload.DisplayName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("DisplayName is required")
            .MaximumLength(100)
            .WithMessage("Display name no longer than 100 characters");
        
        RuleFor(command => command.Payload.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("PhoneNumber cannot be null or empty.")
            .Matches(Common.ValidPhoneRegex)
            .WithMessage("Phone is invalid");
        
        RuleFor(command => command.Payload.Avatar)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Avatar cannot be null or empty.");
    }
}
