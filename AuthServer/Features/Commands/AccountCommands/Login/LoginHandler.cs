using System.Net;
using AuthServer.Data.Models;
using AuthServer.Models.Responses;
using AuthServer.Repositories;
using IdentityModel.Client;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Responses;

namespace AuthServer.Features.Commands.AccountCommands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly UserManager<Account> _userManager;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(IUnitOfRepository unitOfRepository, UserManager<Account> userManager, ILogger<LoginHandler> logger)
    {
        _unitOfRepository = unitOfRepository;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var functionName = nameof(LoginHandler);
        _logger.LogInformation($"{functionName} => ");
        var loginResponse = new LoginResponse(){StatusCode = (int)ResponseStatusCode.BadRequest};
        try
        {
            var payload = request.Payload;
            var user = await _unitOfRepository.Account
                .Where(u => u.UserName == payload.UserName)
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                _logger.LogError($"{functionName} => User not found");
                loginResponse.StatusCode = (int)ResponseStatusCode.NotFound;
                loginResponse.ErrorMessage = "User not found";
                return loginResponse;
            }
            
            if (!user.IsActive)
            {
                _logger.LogError($"{functionName} => User is deleted");
                loginResponse.ErrorMessage = "User is deleted";
                return loginResponse;
            }
            
            var roles = await _userManager.GetRolesAsync(user);
            var client = await _unitOfRepository.Client
                .Where(cl => cl.Id == user.ClientId)
                .Select(client => new Clients
                {
                    Id = client.Id,
                    ClientId = client.ClientId
                })
                .FirstOrDefaultAsync(cancellationToken);
            
            var clientSecret = await _unitOfRepository.ClientSecret
                .Where(cs => cs.ClientId == client.Id)
                .Select(cs => cs.SecretName).FirstOrDefaultAsync(cancellationToken);
            
            var scopes = await _unitOfRepository.RolePermission
                .Where(p => p.Role == roles[0])
                .Select(p => p.Permission).ToListAsync(cancellationToken);
            var requestScopes = "";
            
            foreach (var scope in scopes)
            {
                requestScopes += " " + scope;
            }
            
            var httpClient = new HttpClient();
            var discovery = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5237", cancellationToken);
            if (discovery.IsError)
            {
                throw new Exception($"Can't get discovery details : Message = {discovery.Error}");
            }

            var response = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = client.ClientId,
                ClientSecret = clientSecret,
                Scope = requestScopes.Trim(),
                UserName = payload.UserName,
                Password = payload.Password
            }, cancellationToken);
            
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                loginResponse.StatusCode = (int)ResponseStatusCode.Ok;
                loginResponse.Data = new LoginResponseData()
                {
                    AccessToken = response.AccessToken,
                    Scope = response.Scope,
                    Expired = response.ExpiresIn
                };
            }
            else
            {
                _logger.LogError($"{functionName} => Can't get access token : Message = {response.ErrorDescription}");
                loginResponse.StatusCode = (int)response.HttpStatusCode;
                loginResponse.ErrorMessage = response.ErrorDescription;
            }
            _logger.LogInformation($"{functionName} - End");
            return loginResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} => Error : Message = {ex.Message}");
            loginResponse.StatusCode = (int)ResponseStatusCode.InternalServerError;
            loginResponse.ErrorMessage = "An error has occured";
            loginResponse.ErrorMessage = ex.Message;
            return loginResponse;
        }
    }
}