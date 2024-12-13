using FluentValidation;
using Shared.Constants;

namespace AuthServer.Features.Commands.AccountCommands.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(command => command)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Command cannot be null or empty.");
        
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
            .WithMessage("Email is not valid.");
        
        RuleFor(command => command.Payload.Password)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password cannot be null or empty.")
            .MaximumLength(16)
            .MinimumLength(8)
            .WithMessage("Password must be between 8 and 16 characters.");
    }
}