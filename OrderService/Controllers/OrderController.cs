using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Features.Commands.OrderCommands.CancelOrder;
using OrderService.Features.Commands.OrderCommands.CheckoutOrder;
using OrderService.Features.Commands.OrderCommands.GetOrderInitial;
using OrderService.Features.Commands.OrderCommands.RejectOrder;
using OrderService.Features.Queries.OrderQueries.GetOrderByStatusEmployee;
using OrderService.Features.Queries.OrderQueries.GetOrdersByStatus;
using OrderService.Models.Requests;
using Shared.Enums;
using Shared.Responses;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet("GetByStatus")]
    public async Task<IActionResult> GetByStatus([FromQuery] OrderStatus status, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOrderByStatusQuery(status), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }
    
    [Authorize]
    [HttpGet("Employee-GetByStatus")]
    public async Task<IActionResult> GetByStatusForEmployee([FromQuery] OrderStatus status, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOrderByStatusEmployeeQuery(status), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }
    
    [Authorize]
    [HttpGet("GetOrderInit/{restaurantId}")]
    public async Task<IActionResult> GetOrderInit([FromRoute] string restaurantId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOrderInitialCommand(restaurantId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }
    
    [Authorize]
    [HttpPost("Checkout")]
    public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutOrderRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CheckoutOrderCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [Authorize]
    [HttpGet("Cancel/{orderId}")]
    public async Task<IActionResult> CancelOrder(int orderId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CancelOrderCommand(orderId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [Authorize]
    [HttpGet("Reject/{orderId}")]
    public async Task<IActionResult> RejectOrder(int orderId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RejectOrderCommand(orderId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
}