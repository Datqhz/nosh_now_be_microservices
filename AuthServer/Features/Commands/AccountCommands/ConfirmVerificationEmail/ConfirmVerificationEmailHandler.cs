using AuthServer.Data.Models;
using AuthServer.Models.Responses;
using AuthServer.Repositories;
using AuthServer.Translations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Responses;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AuthServer.Features.Commands.AccountCommands.ConfirmVerificationEmail;

public class ConfirmVerificationEmailHandler : IRequestHandler<ConfirmVerificationEmailCommand, ConfirmVerificationEmailResponse>
{
    private readonly UserManager<Account> _accountManager;
    private readonly ILogger<ConfirmVerificationEmailHandler> _logger;
    private readonly IUnitOfRepository _unitOfRepository;

    public ConfirmVerificationEmailHandler
    (
        UserManager<Account> accountManager,
        ILogger<ConfirmVerificationEmailHandler> logger,
        IUnitOfRepository unitOfRepository
    )
    {
        _accountManager = accountManager;
        _logger = logger;
        _unitOfRepository = unitOfRepository;
    }
    public async Task<ConfirmVerificationEmailResponse> Handle(ConfirmVerificationEmailCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName =
            $"{nameof(ConfirmVerificationEmailHandler)} Payload = {JsonSerializer.Serialize(payload)}=> ";
        _logger.LogInformation(functionName);
        var response = new ConfirmVerificationEmailResponse() { StatusCode = (int)ResponseStatusCode.BadRequest };

        try
        {
            var account = await _unitOfRepository.Account
                .Where(u => !u.IsActive
                             && u.NormalizedUserName.Equals(payload.Email.ToUpper()))
                .FirstOrDefaultAsync(cancellationToken)!;

            if (account is null)
            {
                _logger.LogError($"{functionName} account not found");
                response.ErrorMessage = AuthServerTranslation.ACC_ERR_01.ToString();
                response.MessageCode = AuthServerTranslation.ACC_ERR_01.ToString();
                return response;
            }

            if (account.EmailConfirmed)
            {
                _logger.LogWarning($"{functionName} email already verified");
                response.ErrorMessage = AuthServerTranslation.VRE_ERR_01.ToString();
                response.MessageCode = AuthServerTranslation.VRE_ERR_01.ToString();
                return response;
            }

            var confirmEmailResult = await _accountManager.ConfirmEmailAsync(account, payload.VerificationToken);
            if (confirmEmailResult.Errors.Any())
            {
                _logger.LogError(
                    $"{functionName} has error: Errors = {JsonSerializer.Serialize(confirmEmailResult.Errors)}");
                response.ErrorMessage = AuthServerTranslation.VRE_ERR_02.ToString();
                response.MessageCode = AuthServerTranslation.VRE_ERR_02.ToString();
                return response;
            }
            
            var accountUpdated = await _unitOfRepository.Account
                .Where(u => u.Id == account.Id)
                .FirstOrDefaultAsync(cancellationToken);
            accountUpdated.IsActive = true;
            _unitOfRepository.Account.Update(accountUpdated);
            await _unitOfRepository.CompleteAsync();
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
            response.AccountId = account.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"{functionName} Error: {ex.Message}");
            response.ErrorMessage = AuthServerTranslation.EXH_ERR_01.ToString();
            response.MessageCode = AuthServerTranslation.EXH_ERR_01.ToString();
        }

        return response;
    }
}