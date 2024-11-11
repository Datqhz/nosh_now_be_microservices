using System.Text.Json;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Repositories;
using Shared.Enums;
using Shared.MassTransits.Contracts;

namespace OrderService.Consumers;

public class UpdateUserConsumer : IConsumer<UpdateSnapshotUser>
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
    
    public async Task Consume(ConsumeContext<UpdateSnapshotUser> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(UpdateUserConsumer)} Message: {JsonSerializer.Serialize(message)}";
        try
        {
            _logger.LogInformation(functionName);
            switch (message.Role)
            {
                case SystemRole.Admin:
                {
                    break;
                }
                case SystemRole.Customer:
                {
                    var customer = await _unitOfRepository.Customer
                        .Where(x => x.Id == message.Id)
                        .FirstOrDefaultAsync();
                    customer.Name = message.Name;
                    customer.Avatar = message.Avatar;
                    _unitOfRepository.Customer.Update(customer);
                    
                    break;
                }
                case SystemRole.Restaurant:
                {
                    var restaurant = await _unitOfRepository.Restaurant
                        .Where(x => x.Id == message.Id)
                        .FirstOrDefaultAsync();
                    restaurant.Name = message.Name;
                    restaurant.Avatar = message.Avatar;
                    restaurant.Coordinate = message.Coordinate;
                    restaurant.Phone = message.Phone;
                    _unitOfRepository.Restaurant.Update(restaurant);
                    await _unitOfRepository.CompleteAsync();
                    break;
                }
                case SystemRole.Chef:
                case SystemRole.ServiceStaff:
                {
                    var employee = await _unitOfRepository.Employee
                        .Where(x => x.Id == message.Id)
                        .FirstOrDefaultAsync();
                    employee.Name = message.Name;
                    employee.Avatar = message.Avatar;
                    _unitOfRepository.Employee.Update(employee);
                    await _unitOfRepository.CompleteAsync();
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