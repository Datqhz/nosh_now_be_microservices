using System.Text.Json;
using CoreService.Data.Models;
using CoreService.Data.Models.Interfaces;
using CoreService.Repositories;
using MassTransit;
using Shared.Enums;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;

namespace CoreService.Consumers;

public class CreateUserConsumer : IConsumer<CreateUser>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CreateUserConsumer> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public CreateUserConsumer
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CreateUserConsumer> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    
    public async Task Consume(ConsumeContext<CreateUser> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(CreateUserConsumer)} Message: {JsonSerializer.Serialize(message)} => ";
        try
        {
            _logger.LogInformation(functionName);
            IUser user = null;
            switch (message.Role)
            {
                case SystemRole.Customer:
                {
                    user = new Customer
                    {
                        Id = new Guid(message.Id),
                        DisplayName = message.DisplayName,
                        PhoneNumber = message.PhoneNumber,
                        Email = message.Email,
                        Avatar = message.Avatar,
                    };
                    await _unitOfRepository.Customer.Add((Customer)user);
                    break;
                }
                case SystemRole.Restaurant:
                {
                    user = new Restaurant
                    {
                        Id = new Guid(message.Id),
                        DisplayName = message.DisplayName,
                        PhoneNumber = message.PhoneNumber,
                        Email = message.Email,
                        Avatar = message.Avatar,
                        Coordinate = message.Coordinate,
                    };
                    await _unitOfRepository.Restaurant.Add((Restaurant)user);
                    break;
                }
                case SystemRole.Chef:
                case SystemRole.ServiceStaff:
                {
                    user = new Employee
                    {
                        Id = new Guid(message.Id),
                        DisplayName = message.DisplayName,
                        PhoneNumber = message.PhoneNumber,
                        Email = message.Email,
                        Avatar = message.Avatar,
                        IsActive = true,
                        RestaurantId = new Guid(message.RestaurantId),
                        Role = SystemRole.ServiceStaff == message.Role ? RestaurantRole.ServiceStaff : RestaurantRole.Chef,
                    };
                    await _unitOfRepository.Employee.Add((Employee)user);
                    break;
                }
                default:
                {
                    throw new Exception($"Unknown role: {message.Role}");
                }
            }

            await _unitOfRepository.CompleteAsync();
            var snapshotUser = new CreateSnapshotUser
            {
                Id = user.Id.ToString(),
                DisplayName = message.DisplayName,
                Avatar = message.Avatar,
                Phone = message.PhoneNumber,
                Role = message.Role,
                RestaurantId = message.RestaurantId,
                Coordinate = message.Coordinate,
            };
            await _sendEndpoint.SendMessage<CreateSnapshotUser>(snapshotUser, ExchangeType.Direct,
                new CancellationToken());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {message}");
        }
    }
}