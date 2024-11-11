using System.Text.Json;
using MassTransit;
using OrderService.Data.Models;
using OrderService.Repositories;
using Shared.Enums;
using Shared.MassTransits.Contracts;

namespace OrderService.Consumers;

public class CreateUserConsumer : IConsumer<CreateSnapshotUser>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CreateUserConsumer> _logger;
    public CreateUserConsumer
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CreateUserConsumer> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<CreateSnapshotUser> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(CreateUserConsumer)} Message: {JsonSerializer.Serialize(message)} => ";
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
                    var user = new Customer
                    {
                        Id = message.Id,
                        Name = message.DisplayName,
                        Avatar = message.Avatar,
                    };
                    await _unitOfRepository.Customer.Add(user);
                    break;
                }
                case SystemRole.Restaurant:
                {
                    var user = new Restaurant
                    {
                        Id = message.Id,
                        Name = message.DisplayName,
                        Phone = message.Phone,
                        Avatar = message.Avatar,
                        Coordinate = message.Coordinate,
                    };
                    await _unitOfRepository.Restaurant.Add(user);
                    break;
                }
                case SystemRole.Chef:
                case SystemRole.ServiceStaff:
                {
                    var user = new Employee
                    {
                        Id = message.Id,
                        Name = message.DisplayName,
                        Avatar = message.Avatar,
                        Role = SystemRole.ServiceStaff == message.Role ? RestaurantRole.ServiceStaff : RestaurantRole.Chef,
                    };
                    await _unitOfRepository.Employee.Add(user);
                    break;
                }
                default:
                {
                    throw new Exception($"Unknown role: {message.Role}");
                }
            }
            await _unitOfRepository.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {message}");
        }
    }
}