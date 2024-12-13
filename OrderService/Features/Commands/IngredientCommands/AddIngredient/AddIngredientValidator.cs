using FluentValidation;
using OrderService.Features.Commands.FoodCommands.AddFood;

namespace OrderService.Features.Commands.IngredientCommands.AddIngredient;

public class AddIngredientValidator : AbstractValidator<AddIngredientCommand>
{
    public AddIngredientValidator()
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
        
        RuleFor(command => command.Payload.IngredientName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Ingredient name is required");
        
        RuleFor(command => command.Payload.Quantity)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Quantity is required")
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
        
        RuleFor(command => command.Payload.Image)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Ingredient image is required");
        
        RuleFor(command => command.Payload.Unit)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Unit is required")
            .IsInEnum()
            .WithMessage("Invalid data value");
    }
}
