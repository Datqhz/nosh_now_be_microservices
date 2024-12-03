using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Features.Commands.OrderCommands.CancelOrder;
using OrderService.Features.Commands.OrderCommands.CheckoutOrder;
using OrderService.Features.Commands.OrderCommands.GetOrderInitial;
using OrderService.Features.Commands.OrderCommands.RejectOrder;
using OrderService.Features.Queries.OrderQueries.GetOrderById;
using OrderService.Features.Queries.OrderQueries.PrepareOrder;
using OrderService.Features.Queries.OrderQueries.GetOrderByStatusEmployee;
using OrderService.Features.Queries.OrderQueries.GetOrdersByStatus;
using OrderService.Models.Requests;
using OrderService.Repositories;
using OrderService.Services;
using Shared.Enums;
using Shared.Responses;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly IShipperSimulation _shipperSimulation;
    public OrderController
    (
        IMediator mediator,
        IUnitOfRepository unitOfRepository,
        IShipperSimulation shipperSimulation
        
    )
    {
        _mediator = mediator;
        _unitOfRepository = unitOfRepository;
        _shipperSimulation = shipperSimulation;
    }

    [Authorize]
    [HttpGet("GetByStatus")]
    public async Task<IActionResult> GetByStatus([FromQuery] GetOrderByStatusRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOrderByStatusQuery(request), cancellationToken);
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
    
    [HttpGet("PrepareOrder/{orderId}")]
    public async Task<IActionResult> GetOrderInitById([FromRoute] long orderId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new PrepareOrderQuery(orderId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }
    
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById([FromRoute] long orderId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOrderByIdQuery(orderId), cancellationToken);
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
    
    [HttpGet("Simulation/{orderId}")]
    public async Task<IActionResult> TestSimulation([FromRoute] long orderId, CancellationToken cancellationToken)
    {
        var order = await _unitOfRepository.Order
            .Where(x => x.Id == orderId && x.Status != OrderStatus.Init && x.Status != OrderStatus.Failed)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        Task.Run(async () =>
        {
            await _shipperSimulation.HandleOrderAsync(order, 12, 12);
        });
        
        return Ok();
    }
}