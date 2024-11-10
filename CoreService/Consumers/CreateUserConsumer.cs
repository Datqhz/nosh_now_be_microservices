using CoreService.Data.Models;
using CoreService.Repositories;
using MassTransit;
using Shared.Enums;
using Shared.MassTransits.Contracts;

namespace CoreService.Consumers;

public class CreateUserConsumer : IConsumer<CreateUser>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CreateUserConsumer> _logger;
    public async Task Consume(ConsumeContext<CreateUser> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(CreateUserConsumer)} Message: {message} => ";
        try
        {
            _logger.LogInformation(functionName);
            switch (message.Role)
            {
                case SystemRole.Admin:
                {
                    var user = new bool();
                    break;
                }
                case SystemRole.Customer:
                {
                    var user = new Customer
                    {
                        DisplayName = message.DisplayName,
                        PhoneNumber = message.PhoneNumber,
                        Email = message.Email,
                        Avatar = message.Avatar,
                        AccountId = message.Id
                    };
                    await _unitOfRepository.Customer.Add(user);
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {message}");
        }
    }
}