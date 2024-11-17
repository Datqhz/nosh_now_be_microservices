using AuthServer.Data.Models;
using AuthServer.Models.Responses;
using AuthServer.Repositories;
using AuthServer.Translations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.Responses;

namespace AuthServer.Features.Commands.AccountCommands.Register;


public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly UserManager<Account> _userManager;
    private readonly ILogger<RegisterHandler> _logger;
    public RegisterHandler
    (
        IUnitOfRepository unitOfRepository,
        UserManager<Account> userManager, 
        ILogger<RegisterHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _userManager = userManager;
        _logger = logger;
    }
    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var functionName = nameof(RegisterHandler);
        _logger.LogInformation($"{functionName} - Start");
        var response = new RegisterResponse(){StatusCode = (int)ResponseStatusCode.BadRequest};
        var payload = request.Payload;
        try
        {
            
            var user = await _unitOfRepository.Account
                .Where(u => u.UserName.ToLower().Equals(payload.UserName.ToLower()))
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (user != null)
            {
                _logger.LogError($"{functionName} => UserName is already in used");
                response.ErrorMessage = "UserName is already in used";
                return response;
            }
            var accountId = Guid.NewGuid().ToString();
            var newAccount = new Account
            {
                Id = accountId,
                ClientId = 1,
                CreatedDate = DateTime.Now,
                UserName = payload.UserName,
                PhoneNumber = payload.PhoneNumber,
                Email = payload.UserName,
                IsActive = false
            };
            
            var hashedPassword = new PasswordHasher<Account>().HashPassword(newAccount, payload.Password); 
            newAccount.PasswordHash = hashedPassword;
            var createResult = await _userManager.CreateAsync(newAccount);
            
            var role = payload.Role.ToUpper();
            var createUserRole = await _userManager.AddToRoleAsync(newAccount, role);
            if (!createResult.Succeeded || !createUserRole.Succeeded)
            {
                throw new Exception($"Failed to create user : Message = {createResult.Errors.ToString() + createUserRole.Errors}");
            }
            
            response.StatusCode = (int)ResponseStatusCode.Created;
            response.Data = new RegisterPostProcessorPayload
            {
                Avatar = payload.Avatar,
                Account = newAccount,
                DisplayName = payload.Displayname,
                Role = GetSystemRole(role),
                Coordinate = payload.Coordinate
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} => Error : Message = {ex.Message}");
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
            response.ErrorMessage = AuthServerTranslation.EXH_ERR_01.ToString();
            response.MessageCode = AuthServerTranslation.EXH_ERR_01.ToString();
        }

        return response;
    }

    #region Private methods

    private SystemRole GetSystemRole(string role)
    {switch (role)
             {
                 case AuthServer.Constants.Constants.Role.Admin:
                     return SystemRole.Admin;
                 case AuthServer.Constants.Constants.Role.Customer:
                     return SystemRole.Customer;
                 case AuthServer.Constants.Constants.Role.ServiceStaff:
                     return SystemRole.ServiceStaff;
                 case AuthServer.Constants.Constants.Role.Restaurant:
                     return SystemRole.Restaurant;
                 case AuthServer.Constants.Constants.Role.Chef:
                     return SystemRole.Chef;
                 default:
                     return SystemRole.Shipper;
             }
    }

    #endregion
}