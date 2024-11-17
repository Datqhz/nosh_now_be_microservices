using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Models;
using OrderService.Features.Commands.OrderDetailCommands.UpdateOrderDetail;
using OrderService.Features.Commands.OrderDetailCommands.UpdatePrepareStatus;
using OrderService.Models.Requests;
using Shared.Responses;

namespace OrderService.Controllers;

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

    [HttpPut]
    public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetailRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateOrderDetailCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPut("PrepareStatus")]
    public async Task<IActionResult> UpdatePrepareStatus([FromBody] UpdatePrepareStatusRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdatePrepareStatusCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
}