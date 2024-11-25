using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.DeleteOrderDetail;

public class DeleteOrderDetailHandler : IRequestHandler<DeleteOrderDetailCommand, DeleteOrderDetailResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<DeleteOrderDetailHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public DeleteOrderDetailHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<DeleteOrderDetailHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<DeleteOrderDetailResponse> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var orderDetailId = request.OrderDetailId;
        var functionName = $"{nameof(DeleteOrderDetailHandler)} OrderDetailId : {orderDetailId}";
        var response = new DeleteOrderDetailResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            var orderDetail = await _unitOfRepository.OrderDetail.GetById(orderDetailId);
            if (orderDetail is null)
            {
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = "Order detail could be found.";
                return response;
            }
            
            var order = await _unitOfRepository.Order
                .Where(x => x.Id == orderDetail.OrderId && x.CustomerId == currentUserId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (order is null)
            {
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                response.ErrorMessage = "Not have permission to Delete order detail.";
                return response;
            }
            
            _unitOfRepository.OrderDetail.Delete(orderDetail);
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