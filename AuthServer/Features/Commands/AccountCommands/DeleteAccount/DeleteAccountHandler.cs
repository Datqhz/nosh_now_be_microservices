using AuthServer.Models.Responses;
using AuthServer.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.Responses;

namespace AuthServer.Features.Commands.AccountCommands.DeleteAccount;

public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, DeleteAccountResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<DeleteAccountHandler> _logger;

    public DeleteAccountHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<DeleteAccountHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async Task<DeleteAccountResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var functionName = $"{nameof(DeleteAccountHandler)} AccountId = {accountId} =>";
        var response = new DeleteAccountResponse(){StatusCode = (int)ResponseStatusCode.Ok};

        try
        {
            _logger.LogInformation(functionName);
            var account = await _unitOfRepository.Account
                .Where(x =>
                    x.Id.Equals(accountId)
                    && x.IsActive
                )
                .FirstOrDefaultAsync(cancellationToken);

            if (account is null)
            {
                _logger.LogError($"{functionName} Account {accountId} not found");
            }
            
            account.IsActive = false;
            _unitOfRepository.Account.Update(account);
            await _unitOfRepository.CompleteAsync();
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}
