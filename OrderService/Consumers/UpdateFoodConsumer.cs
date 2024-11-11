using System.Text.Json;
using MassTransit;
using OrderService.Data.Models;
using OrderService.Repositories;
using Shared.MassTransits.Contracts;

namespace OrderService.Consumers;

public class UpdateFoodConsumer : IConsumer<UpdateFood>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateFoodConsumer> _logger;
    public UpdateFoodConsumer
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateFoodConsumer> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<UpdateFood> context)
    {
        var message = context.Message;
        var functionName = $"{nameof(UpdateFoodConsumer)} Message: {JsonSerializer.Serialize(message)} => ";
        try
        {
            _logger.LogInformation(functionName);
            var food = await _unitOfRepository.Food.GetById(message.Id);
            food.Name = message.Name;
            food.Image = message.Image;
            _unitOfRepository.Food.Update(food);
            await _unitOfRepository.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {message}");
        }
    }
}