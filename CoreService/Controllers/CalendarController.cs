using CoreService.Features.Commands.CalendarCommands.AddCalendars;
using CoreService.Features.Commands.CalendarCommands.DeleteCalendars;
using CoreService.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace CoreService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CalendarController : ControllerBase
{
    private readonly IMediator _mediator;
    public CalendarController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpPost("AddCalendars")]
    public async Task<IActionResult> AddCalendars([FromBody] AddCalendarsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddCalendarsCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPut("DeleteCalendars")]
    public async Task<IActionResult> DeleteCalendars([FromBody] DeleteCalendarsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteCalendarsCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
}