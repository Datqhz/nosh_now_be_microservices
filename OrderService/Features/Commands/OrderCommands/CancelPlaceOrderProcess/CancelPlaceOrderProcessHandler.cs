using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;

namespace OrderService.Features.Commands.OrderCommands.CancelPlaceOrderProcess;

public class CancelPlaceOrderProcessHandler : IRequestHandler<CancelPlaceOrderProcessCommand>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CancelPlaceOrderProcessHandler> _logger;
    public CancelPlaceOrderProcessHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CancelPlaceOrderProcessHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task Handle(CancelPlaceOrderProcessCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(CancelPlaceOrderProcessHandler)} OrderId: {request.OrderId} =>";

        try
        {
            await using var transaction = await _unitOfRepository.OpenTransactionAsync();
            
            var order = await _unitOfRepository.Order
                .Where(x => x.Id == request.OrderId && x.Status == OrderStatus.Init)
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
            {
                return;
            }
            
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
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
        }
    }
}