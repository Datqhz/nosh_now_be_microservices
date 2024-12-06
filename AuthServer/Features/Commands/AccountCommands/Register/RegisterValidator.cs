﻿using FluentValidation;

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
            .WithMessage("UserName cannot be null or empty.");
        RuleFor(command => command.Payload.Displayname)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("DisplayName cannot be null or empty.");
        RuleFor(command => command.Payload.Password)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password cannot be null or empty.");
        RuleFor(command => command.Payload.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("PhoneNumber cannot be null or empty.");
    }
}