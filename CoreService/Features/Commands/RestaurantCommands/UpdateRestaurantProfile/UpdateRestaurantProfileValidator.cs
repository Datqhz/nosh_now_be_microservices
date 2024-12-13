using FluentValidation;
using Shared.Constants;

namespace CoreService.Features.Commands.RestaurantCommands.UpdateRestaurantProfile;

public class UpdateRestaurantProfileValidator : AbstractValidator<UpdateRestaurantProfileCommand>
{
    public UpdateRestaurantProfileValidator()
    {
        RuleFor(command => command)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Command is required");
        
        RuleFor(command => command.Payload)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Payload is required");
        
        RuleFor(command => command.Payload.DisplayName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Display name is required")
            .MaximumLength(100)
            .WithMessage("Display name no longer than 100 characters");
        
        RuleFor(command => command.Payload.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(Common.ValidPhoneRegex)
            .WithMessage("Phone number is invalid");

        RuleFor(command => command.Payload.Coordinate)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Coordinate is required");
        
        RuleFor(command => command.Payload.Avatar)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Avatar is required");
    }
}
