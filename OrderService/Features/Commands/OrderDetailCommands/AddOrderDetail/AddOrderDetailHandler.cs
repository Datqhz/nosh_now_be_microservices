using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;
using OrderService.Enums;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.AddOrderDetail;

public class AddOrderDetailHandler : IRequestHandler<AddOrderDetailCommand, CreateOrderDetailResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<AddOrderDetailHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public AddOrderDetailHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AddOrderDetailHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<CreateOrderDetailResponse> Handle(AddOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(AddOrderDetailHandler)} Payload : {JsonSerializer.Serialize(payload)} =>";
        var response = new CreateOrderDetailResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var order = await _unitOfRepository.Order
                .Where(x => 
                    x.Id == payload.OrderId 
                    && x.Status == OrderStatus.Init 
                    && x.CustomerId == currentUserId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (order is null)
            {
                response.ErrorMessage = "Order could be found";
                return response;
            }
            
            var orderDetail = await _unitOfRepository.OrderDetail
                .Where(x => 
                    x.OrderId == order.Id
                    && x.FoodId == payload.FoodId
                )
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            
            if (orderDetail is not null)
            {
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = "Order detail already exists";
                return response;
            }

            orderDetail = new OrderDetail
            {
                OrderId = order.Id,
                Amount = payload.Amount,
                FoodId = payload.FoodId,
                Status = PrepareStatus.Preparing,
                Price = 0
            };
            var newOrder = await _unitOfRepository.OrderDetail.Add(orderDetail);
            await _unitOfRepository.CompleteAsync();
            response.StatusCode = (int)ResponseStatusCode.Created;
            response.Data = newOrder.Id;
        }
        catch (Exception exception)
        {
            exception.LogError(functionName, _logger);
        }

        return response;
    }
}