
using System.Text.Json;
using CoreService.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.MassTransits.Contracts;

namespace CoreService.Consumers;

public class UpdateUserConsumer : IConsumer<UpdateUser>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateUserConsumer> _logger;
    public UpdateUserConsumer
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateUserConsumer> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<UpdateUser> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(UpdateUserConsumer)} Message: {JsonSerializer.Serialize(message)}";
        try
        {
            _logger.LogInformation(functionName);
            switch (message.SystemRole)
            {
                case SystemRole.Customer:
                {
                    var customer = await _unitOfRepository.Customer
                        .Where(x => x.Id.ToString() == message.AccountId)
                        .FirstOrDefaultAsync();
                    customer.IsActive = message.IsActive;
                    _unitOfRepository.Customer.Update(customer);
                    await _unitOfRepository.CompleteAsync();
                    break;
                }
                case SystemRole.Restaurant:
                {
                    var restaurant = await _unitOfRepository.Restaurant
                        .Where(x => x.Id.ToString() == message.AccountId)
                        .FirstOrDefaultAsync();
                    restaurant.IsActive = message.IsActive;
                    _unitOfRepository.Restaurant.Update(restaurant);
                    await _unitOfRepository.CompleteAsync();
                    break;
                }
                case SystemRole.Chef:
                case SystemRole.ServiceStaff:
                {
                    var employee = await _unitOfRepository.Employee
                        .Where(x => x.Id.ToString() == message.AccountId)
                        .FirstOrDefaultAsync();
                    employee.IsActive = message.IsActive;
                    _unitOfRepository.Employee.Update(employee);
                    
                    break;
                }
                default:
                    throw new Exception("Unknown SystemRole");
            }
            await _unitOfRepository.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error : {ex.Message}");
        }
    }
}