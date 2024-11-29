using AuthServer.Data.Models;
using AuthServer.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace AuthServer.Features.Commands.AccountCommands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
{
    private readonly UserManager<Account> _userManager;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ChangePasswordHandler> _logger;
    public ChangePasswordHandler
    (
        ICustomHttpContextAccessor httpContextAccessor,
        ILogger<ChangePasswordHandler> logger,
        UserManager<Account> userManager
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _userManager = userManager;
    }
    public async Task<ChangePasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var currentUserId = _httpContextAccessor.GetCurrentUserId();
        var functionName = $"{nameof(ChangePasswordHandler)} UserId = {currentUserId} =>";
        var response = new ChangePasswordResponse(){StatusCode = (int)ResponseStatusCode.Ok};

        try
        {
            _logger.LogInformation(functionName);
            var account = await _userManager.FindByIdAsync(currentUserId);
            if (account is null)
            {
                _logger.LogWarning($"{functionName} User {currentUserId} not found");
                response.Message = "User does not exist";
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                return response;
            }

            var verifyOldPassword = await _userManager.CheckPasswordAsync(account, payload.OldPassword);
            if (!verifyOldPassword)
            {
                _logger.LogWarning($"{functionName} => Old password incorrect");
                response.StatusCode = (int)ResponseStatusCode.BadRequest;
                response.ErrorMessage = "Old password incorrect";
                return response;
            }

            var newHashedPassword = new PasswordHasher<Account>().HashPassword(account, payload.NewPassword);
            account.PasswordHash = newHashedPassword;
            var updateResult = await _userManager.UpdateAsync(account);
            if (!updateResult.Succeeded)
            {
                throw new Exception(updateResult.Errors.ToString());
            }

            return response;
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
            response.ErrorMessage = "Internal server error";
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}
