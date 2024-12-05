using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.UpdatePrepareStatus;

public class UpdatePrepareStatusHandler : IRequestHandler<UpdatePrepareStatusCommand, UpdatePrepareStatusResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdatePrepareStatusHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdatePrepareStatusHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdatePrepareStatusHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<UpdatePrepareStatusResponse> Handle(UpdatePrepareStatusCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdatePrepareStatusHandler)} Payload : {JsonSerializer.Serialize(payload)}";
        var response = new UpdatePrepareStatusResponse {StatusCode = (int)ResponseStatusCode.NotFound};

        try
        {
            _logger.LogInformation(functionName);
            var currentUserId = _httpContextAccessor.GetCurrentUserId();

            if (!payload.Input.Any())
            {
                response.StatusCode = (int)ResponseStatusCode.Ok;
                return response;
            }

            var order = await _unitOfRepository.Order
                .Where(x => x.Id == payload.OrderId)
                .FirstOrDefaultAsync(cancellationToken);
            if (order is null)
            {
                response.ErrorMessage = "Order not found";
                return response;
            }
            
            var chef = await _unitOfRepository.Employee
                .Where(x => x.Id.Equals(currentUserId) && x.Role == RestaurantRole.Chef &&
                            x.RestaurantId == order.RestaurantId)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (chef is null)
            {
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                response.ErrorMessage = "No have permission";
                return response;
            }

            foreach (var input in payload.Input)
            {
                _logger.LogInformation($"{functionName} detail = {input.OrderDetailId}");
                var orderDetail = await _unitOfRepository.OrderDetail.GetById(input.OrderDetailId);
                orderDetail.Status = input.Status;
                _unitOfRepository.OrderDetail.Update(orderDetail);
            }

            await _unitOfRepository.CompleteAsync();
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }
        
        return response;
    }
}