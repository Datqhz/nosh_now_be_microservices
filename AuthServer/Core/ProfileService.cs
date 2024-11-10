using System.Security.Claims;
using AuthServer.Data.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Core;

public class ProfileService : IProfileService
{
    private readonly UserManager<Account> _accountManager;
    private readonly ILogger<ProfileService> _logger;

    public ProfileService(UserManager<Account> accountManager, ILogger<ProfileService> logger)
    {
        _accountManager = accountManager;
        _logger = logger;
    }
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        try
        {
            var clientId = context.Client.ClientId;
            var sub = context.Subject.GetSubjectId();
            var account = await _accountManager.FindByIdAsync(sub);
            var roles = await _accountManager.GetRolesAsync(account);
            var claims = new List<Claim>()
            {
                new Claim(Shared.Constants.Constants.CustomClaimTypes.AccountId, sub),
                new Claim(Shared.Constants.Constants.CustomClaimTypes.Role, roles[0]),
                new Claim(Shared.Constants.Constants.CustomClaimTypes.UserName, account.UserName),
                new Claim(Shared.Constants.Constants.CustomClaimTypes.ClientId, clientId)
            };
            context.IssuedClaims.AddRange(claims);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(ProfileService)} Has error: Message = {ex.Message}");
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var account = await _accountManager.FindByIdAsync(sub);
        context.IsActive = account != null;
    }
}