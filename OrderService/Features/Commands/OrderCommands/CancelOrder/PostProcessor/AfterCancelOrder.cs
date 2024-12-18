﻿using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderCommands.CancelOrder.PostProcessor;

public class AfterCancelOrder : IRequestPostProcessor<CancelOrderCommand, CancelOrderResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<AfterCancelOrder> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    public AfterCancelOrder
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AfterCancelOrder> logger,
        ISendEndpointCustomProvider sendEndpoint
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _sendEndpoint = sendEndpoint;
    }
    public async Task Process(CancelOrderCommand request, CancelOrderResponse response, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(AfterCancelOrder)} OrderId = {request.OrderId} =>";
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
            }
        }
        catch (Exception ex)
        {
            await _unitOfRepository.RollbackAsync();
            ex.LogError(functionName, _logger);
            await _unitOfRepository.RollbackAsync();
        }
    }
}