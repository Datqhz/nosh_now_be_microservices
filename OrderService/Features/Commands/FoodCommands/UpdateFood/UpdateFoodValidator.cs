using FluentValidation;

namespace OrderService.Features.Commands.FoodCommands.UpdateFood;

public class UpdateFoodValidator : AbstractValidator<UpdateFoodCommand>
{
    public UpdateFoodValidator()
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

        When(command => command.Payload.Ingredients.Any(), () =>
        {
            RuleForEach(command => command.Payload.Ingredients)
                .Cascade(CascadeMode.Stop)
                .Must(x => x.Quantity > 0)
                .WithMessage("Quantity must be greater than 0");
        });
    }
}
