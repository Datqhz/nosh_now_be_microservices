using FluentValidation;

namespace OrderService.Features.Commands.FoodCommands.AddFood;

public class AddFoodValidator : AbstractValidator<AddFoodCommand>
{
    public AddFoodValidator()
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
        
        RuleFor(command => command.Payload.FoodName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Food name is required");
        
        RuleFor(command => command.Payload.FoodImage)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Food image is required");
        
        RuleFor(command => command.Payload.CategoryId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Category id is required");
        
        RuleFor(command => command.Payload.FoodPrice)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Food price is required")
            .GreaterThan(0)
            .WithMessage("Food price must be greater than 0");

        RuleFor(command => command.Payload.Ingredients)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Ingredients is required")
            .ForEach(orderRule =>
            {
                orderRule
                    .Cascade(CascadeMode.Stop)
                    .Must(x => x.Quantity > 0)
                    .WithMessage("Quantity must be greater than 0");
            });
    }
}
