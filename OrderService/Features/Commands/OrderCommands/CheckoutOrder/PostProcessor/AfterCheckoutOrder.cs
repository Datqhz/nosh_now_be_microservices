using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using OrderService.Enums;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderCommands.CheckoutOrder.PostProcessor;

public class AfterCheckoutOrder : IRequestPostProcessor<CheckoutOrderCommand, CheckoutOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<AfterCheckoutOrder> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterCheckoutOrder
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AfterCheckoutOrder> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(CheckoutOrderCommand request, CheckoutOrderResponse response, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(CheckoutOrderHandler)} =>";

        try
        {
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                /* 1. Re-Calculate total ingredient*/
                // foreach (var record in payload.OrderDetails)
                // {
                //     var ingredientsData = await
                //         (
                //             from ri in _unitOfRepository.RequiredIngredient.GetAll()
                //             join i in _unitOfRepository.Ingredient.GetAll()
                //                 on ri.IngredientId equals i.Id
                //             where ri.FoodId == record.FoodId
                //             select new
                //             {
                //                 Ingredient = ri.Ingredient,
                //                 RequiredAmount = ri.Quantity
                //             }
                //         )
                //         .ToListAsync(cancellationToken);
                //     foreach (var ingredient in ingredientsData)
                //     {
                //         ingredient.Ingredient.Quantity -= ingredient.RequiredAmount * record.Amount;
                //         _unitOfRepository.Ingredient.Update(ingredient.Ingredient);
                //     }
                // }
                //
                // await _unitOfRepository.CompleteAsync();
            
                /* 2. Send a message to check status of order after 1 minute */
                var totalPay =  await _unitOfRepository.OrderDetail
                    .Where(x => x.OrderId == payload.OrderId)
                    .SumAsync(x => x.Amount * x.Price, cancellationToken);
                var order = await  _unitOfRepository.Order.GetById(payload.OrderId);
                var receivers = await _unitOfRepository.Employee
                    .Where(x => x.RestaurantId.Equals(order.RestaurantId) && x.Role == RestaurantRole.ServiceStaff)
                    .AsNoTracking()
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);
                var message = new NotifyOrderSchedule
                {
                    Title = $"You have new order",
                    Content = $"Your restaurant receive new order with total pay {totalPay}",
                    Receivers = receivers
                };
                // await _sendEndpoint.SendMessage<NotifyOrderSchedule>(message, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
        }
    }
}