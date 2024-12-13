using FluentValidation;

namespace CoreService.Features.Commands.EmployeeCommands.UpdateEmployeeProfile;

public class UpdateEmployeeProfileValidator : AbstractValidator<UpdateEmployeeProfileCommand>
{
    public UpdateEmployeeProfileValidator()
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
        
        RuleFor(command => command.Payload.EmployeeId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("EmployeeId is required");
        
        RuleFor(command => command.Payload.DisplayName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Display name is required")
            .MaximumLength(100)
            .WithMessage("Display name cannot be longer than 100 characters");
        
        RuleFor(command => command.Payload.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Phone number is required");
        
        RuleFor(command => command.Payload.Avatar)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Avatar is required");
    }
}
