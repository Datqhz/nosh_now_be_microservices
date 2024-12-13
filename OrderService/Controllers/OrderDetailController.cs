using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Models;
using OrderService.Features.Commands.OrderDetailCommands.AddOrderDetail;
using OrderService.Features.Commands.OrderDetailCommands.DeleteOrderDetail;
using OrderService.Features.Commands.OrderDetailCommands.UpdateOrderDetail;
using OrderService.Features.Commands.OrderDetailCommands.UpdatePrepareStatus;
using OrderService.Models.Requests;
using Shared.Responses;

namespace OrderService.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class OrderDetailController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderDetailController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail([FromBody] AddOrderDetailRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddOrderDetailCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetailRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateOrderDetailCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail([FromRoute] long id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteOrderDetailCommand(id), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPut("PrepareStatus")]
    public async Task<IActionResult> UpdatePrepareStatus([FromBody] UpdatePrepareStatusRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdatePrepareStatusCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
}