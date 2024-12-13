using CoreService.Models.Requests;
using FluentValidation;

namespace CoreService.Features.Commands.CalendarCommands.AddCalendars;

public class AddCalendarsValidator : AbstractValidator<AddCalendarsCommand>
{
    public AddCalendarsValidator()
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

        RuleForEach(command => command.Payload.Inputs)
            .SetValidator(new CalendarInputValidator());
    }
}

public class CalendarInputValidator : AbstractValidator<CalendarInput>
{
    public CalendarInputValidator()
    {
        RuleFor(x => x.StartDate)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Start time is required.");
        
        RuleFor(x => x.EndDate)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("End time is required");
        
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .Must(x => 
                x.StartDate.Date > DateTime.Now.Date 
                && x.EndDate.Date < DateTime.Now.Date
            )
            .WithMessage("Start time and end time must be in future.")
            .Must(x => 
                x.StartDate.Day == x.EndDate.Day 
                && x.StartDate.Month == x.EndDate.Month 
                && x.StartDate.Year == x.EndDate.Year
            )
            .WithMessage("Start time and end time must be in a same day")
            .Must(x => x.StartDate.TimeOfDay < x.EndDate.TimeOfDay)
            .WithMessage("Start time must be before end time.");
    }
}
