using AuthServer.Data.Models;
using AuthServer.Models.Responses;
using AuthServer.Repositories;
using MassTransit;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Shared.Enums;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace AuthServer.Features.Commands.AccountCommands.ConfirmVerificationEmail.ConfirmEmailPostProcessor;

public class AfterConfirmEmailPostProcessor : IRequestPostProcessor<ConfirmVerificationEmailCommand, ConfirmVerificationEmailResponse>
{
    private readonly ILogger<AfterConfirmEmailPostProcessor> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    private readonly UserManager<Account> _accountManager;
    public AfterConfirmEmailPostProcessor
    (
        ILogger<AfterConfirmEmailPostProcessor> logger,
        ISendEndpointCustomProvider sendEndpoint,
        UserManager<Account> accountManager
    )
    {
        _logger = logger;
        _sendEndpoint = sendEndpoint;
        _accountManager = accountManager;
    }
    public async Task Process(ConfirmVerificationEmailCommand request, ConfirmVerificationEmailResponse response,
        CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(AfterConfirmEmailPostProcessor)} AccountId = {response.AccountId} => ";
        _logger.LogInformation(functionName);
        try
        {
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                var account = await _accountManager.FindByIdAsync(response.AccountId);
                var role = await _accountManager.GetRolesAsync(account);
                var message = new UpdateUser
                {
                    AccountId = response.AccountId,
                    IsActive = true,
                    SystemRole = GetSystemRole(role.First().ToUpper())
                };
                await _sendEndpoint.SendMessage<UpdateUser>(message, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }
    }

    #region Private methods

    private SystemRole GetSystemRole(string role)
    {
        switch (role)
        {
            case Constants.Constants.Role.Admin:
                return SystemRole.Admin;
            case Constants.Constants.Role.Customer:
                return SystemRole.Customer;
            case Constants.Constants.Role.ServiceStaff:
                return SystemRole.ServiceStaff;
            case Constants.Constants.Role.Restaurant:
                return SystemRole.Restaurant;
            case Constants.Constants.Role.Chef:
                return SystemRole.Chef;
            default:
                return SystemRole.Shipper;
        }
    }

    #endregion
}