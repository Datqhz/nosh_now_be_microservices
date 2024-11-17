using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.UpdateOrderDetail;

public class UpdateOrderDetailHandler : IRequestHandler<UpdateOrderDetailCommand, UpdateOrderDetailResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateOrderDetailHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdateOrderDetailHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateOrderDetailHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<UpdateOrderDetailResponse> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdateOrderDetailHandler)} Payload : {JsonSerializer.Serialize(payload)}";
        var response = new UpdateOrderDetailResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();

            var orderDetail = await _unitOfRepository.OrderDetail.GetById(payload.OrderDetailId);
            if (orderDetail is null)
            {
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                return response;
            }
            
            var order = await _unitOfRepository.Order
                .Where(x => x.Id == orderDetail.OrderId && x.CustomerId == currentUserId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (order is null)
            {
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                return response;
            }
            
            orderDetail.Amount = payload.Amount;
            _unitOfRepository.OrderDetail.Update(orderDetail);
            await _unitOfRepository.CompleteAsync();
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception exception)
        {
            exception.LogError(functionName, _logger);
        }

        return response;
    }
}