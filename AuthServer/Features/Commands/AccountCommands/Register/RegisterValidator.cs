using FluentValidation;
using Shared.Constants;

namespace AuthServer.Features.Commands.AccountCommands.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
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
        
        RuleFor(command => command.Payload.UserName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email cannot be null or empty.")
            .Matches(Common.ValidEmailRegex)
            .WithMessage("Invalid email format.");
        
        RuleFor(command => command.Payload.Displayname)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("DisplayName cannot be null or empty.")
            .MaximumLength(100)
            .WithMessage("DisplayName cannot be longer than 100 characters.");
        
        RuleFor(command => command.Payload.Password)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password cannot be null or empty.")
            .MaximumLength(16)
            .MinimumLength(8)
            .WithMessage("Password must be between 8 and 16 characters.");
        
        RuleFor(command => command.Payload.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Phone number cannot be null or empty.")
            .Matches(Common.ValidPhoneRegex)
            .WithMessage("Phone number isn't valid.");
    }
}