using CommunicationService.Features.Commands.NotifyCommands.NotifyOrderStatusChange;
using CommunicationService.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationService.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpPost("SendNotify")]
    public async Task<IActionResult> SendNotify([FromBody] NotifyOrderStatusChangeRequest request,
        CancellationToken token)
    {
        await _mediator.Send(new NotifyOrderStatusChangeCommand(request), token);
        return Ok();
    }
}