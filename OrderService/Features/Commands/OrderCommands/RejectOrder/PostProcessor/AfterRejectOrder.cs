using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderCommands.RejectOrder.PostProcessor;

public class AfterRejectOrder : IRequestPostProcessor<RejectOrderCommand, RejectOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<AfterRejectOrder> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterRejectOrder
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AfterRejectOrder> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(RejectOrderCommand request, RejectOrderResponse response, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(AfterRejectOrder)} OrderId = {request.OrderId} =>";
        await using var transaction = await _unitOfRepository.OpenTransactionAsync();

        try
        {
            _logger.LogInformation(functionName);
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                var orderDetails = await _unitOfRepository.OrderDetail
                    .Where(x => x.OrderId == request.OrderId)
                    .ToListAsync(cancellationToken);
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
                        ingredient.Ingredient.Quantity += ingredient.Quantity * orderDetail.Amount;
                        _unitOfRepository.Ingredient.Update(ingredient.Ingredient);
                    }
                }

                await _unitOfRepository.CompleteAsync();
                await _unitOfRepository.CommitAsync();
                
                /* Todo: Send message to queue for notify customer*/
                var order = await  _unitOfRepository.Order.GetById(request.OrderId);
                var restaurantName = await _unitOfRepository.Restaurant
                    .Where(x => x.Id.Equals(order.RestaurantId))
                    .AsNoTracking()
                    .Select(x => x.Name)
                    .FirstOrDefaultAsync(cancellationToken);
                var message = new NotifyOrder
                {
                    OrderId = request.OrderId.ToString(),
                    OrderStatus = OrderStatus.Rejected,
                    RestaurantName = restaurantName,
                    Receivers = [order.CustomerId],
                    ReceiverType = ReceiverType.Customer
                };
                await _sendEndpoint.SendMessage<NotifyOrder>(message, ExchangeType.Direct, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            await _unitOfRepository.RollbackAsync();
            ex.LogError(functionName, _logger);
        }
    }
}