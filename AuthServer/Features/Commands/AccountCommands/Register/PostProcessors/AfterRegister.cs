using AuthServer.Data.Models;
using AuthServer.Models.Responses;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace AuthServer.Features.Commands.AccountCommands.Register.PostProcessors;

public class AfterRegisterPostProcessor : IRequestPostProcessor<RegisterCommand, RegisterResponse>
{
    private readonly ILogger<AfterRegisterPostProcessor> _logger;
    private readonly UserManager<Account> _accountManager;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterRegisterPostProcessor
    (
        ILogger<AfterRegisterPostProcessor> logger,
        UserManager<Account> accountManager,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _logger = logger;
        _accountManager = accountManager;
        _sendEndpoint = sendEndpoint;
    }
    
    public async Task Process(RegisterCommand request, RegisterResponse response, CancellationToken cancellationToken)
    {
        const string functionName = $"{nameof(AfterRegisterPostProcessor)} => ";
        try
        {
            Console.WriteLine(functionName);
            if (response.StatusCode == (int)ResponseStatusCode.Created)
            {
                var newAccount = response.Data.Account;
                var confirmationToken = await _accountManager.GenerateEmailConfirmationTokenAsync(newAccount);
                var createUserEvent = new CreateUser
                {
                    Id = newAccount.Id,
                    PhoneNumber = newAccount.PhoneNumber,
                    Email = newAccount.Email,
                    Avatar = response.Data.Avatar,
                    DisplayName = response.Data.DisplayName,
                    Role = response.Data.Role,
                    Coordinate = response.Data.Coordinate
                };
                var sendVerificationEmailEvent = new SendVerificationEmail
                {
                    Email = newAccount.Email,
                    VerificationToken = confirmationToken,
                    DisplayName = response.Data.DisplayName,
                };
                //send
                await _sendEndpoint.SendMessage<CreateUser>(createUserEvent, ExchangeType.Direct, cancellationToken);
                await _sendEndpoint.SendMessage<SendVerificationEmail>(sendVerificationEmailEvent, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }
    }
}