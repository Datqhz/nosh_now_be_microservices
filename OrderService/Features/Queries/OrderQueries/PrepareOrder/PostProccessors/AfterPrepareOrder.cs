using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace OrderService.Features.Queries.OrderQueries.PrepareOrder.PostProccessors;

public class AfterPrepareOrder : IRequestPostProcessor<PrepareOrderQuery, PrepareOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<AfterPrepareOrder> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterPrepareOrder
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AfterPrepareOrder> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(PrepareOrderQuery request, PrepareOrderResponse response, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(AfterPrepareOrder)} =>";

        try
        {
            _logger.LogInformation(functionName);
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                await using var transaction = await _unitOfRepository.OpenTransactionAsync();
                
                /* 1. Get all order details of this order */
                var orderDetails = await _unitOfRepository.OrderDetail
                    .Where(x => x.OrderId == request.OrderId)
                    .ToListAsync(cancellationToken);
                
                /* Calculate after user get order to complete order */
                foreach (var orderDetail in orderDetails)
                {
                    var ingredientData = await
                        (
                            from ri in _unitOfRepository.RequiredIngredient.GetAll()
                            join i in _unitOfRepository.Ingredient.GetAll()
                                on ri.IngredientId equals i.Id
                            where ri.FoodId == orderDetail.FoodId
                            select new
                            {
                                Quantity = ri.Quantity,
                                Ingredient = i
                            }
                        )
                        .ToListAsync(cancellationToken);
                    
                    foreach (var ingredient in ingredientData)
                    {
                        ingredient.Ingredient.Quantity -= ingredient.Quantity * orderDetail.Amount;
                        _unitOfRepository.Ingredient.Update(ingredient.Ingredient);
                    }
                }

                await _unitOfRepository.CompleteAsync();
                await _unitOfRepository.CommitAsync();

                var scheduleMessage = new ReCalculateIngredientSchedule
                {
                    OrderId = request.OrderId,
                    Duration = TimeSpan.FromMinutes(5)
                };

                await _sendEndpoint.SendMessage<ReCalculateIngredientSchedule>(scheduleMessage, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
        }
    }
}