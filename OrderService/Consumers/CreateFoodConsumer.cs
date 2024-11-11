using System.Text.Json;
using MassTransit;
using OrderService.Data.Models;
using OrderService.Repositories;
using Shared.MassTransits.Contracts;

namespace OrderService.Consumers;

public class CreateFoodConsumer : IConsumer<CreateFood>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CreateFoodConsumer> _logger;
    public CreateFoodConsumer
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CreateFoodConsumer> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<CreateFood> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(CreateFoodConsumer)} Message: {JsonSerializer.Serialize(message)} => ";
        try
        {
            _logger.LogInformation(functionName);
            var food = new Food
            {
                Id = message.Id,
                Name = message.Name,
                Image = message.Image,
            };
            await _unitOfRepository.Food.Add(food);
            await _unitOfRepository.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {message}");
        }
    }
}