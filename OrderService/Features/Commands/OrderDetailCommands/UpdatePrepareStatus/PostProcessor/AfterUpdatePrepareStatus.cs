using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using OrderService.Enums;
using OrderService.Models.Requests;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.UpdatePrepareStatus.PostProcessor;

public class AfterUpdatePrepareStatus : IRequestPostProcessor<UpdatePrepareStatusCommand, UpdatePrepareStatusResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<AfterUpdatePrepareStatus> _logger;
    public AfterUpdatePrepareStatus
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AfterUpdatePrepareStatus> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async Task Process(UpdatePrepareStatusCommand request, UpdatePrepareStatusResponse response, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(UpdatePrepareStatusHandler)} OrderId = {request.Payload.OrderId}";

        try
        {
            _logger.LogInformation(functionName);
            if (response.StatusCode == (int)ResponseStatusCode.Ok)
            {
                var orderDetails = await _unitOfRepository.OrderDetail
                    .Where(x => x.OrderId == request.Payload.OrderId && x.Status == PrepareStatus.Done)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                if (!orderDetails.Any())
                {
                    var order = await _unitOfRepository.Order.GetById(request.Payload.OrderId);
                    order.Status = OrderStatus.ReadyToPickup;
                    _unitOfRepository.Order.Update(order);
                    await _unitOfRepository.CompleteAsync();

                    /* Todo: Send message to notify for service staff */

                    /* Todo: Send message to notify for shipper */

                    /* Todo: Send message to notify for customer */
                }
            }
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
        }
    }
}