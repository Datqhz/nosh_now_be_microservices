using CoreService.Features.Commands.CalendarCommands.AddCalendars;
using CoreService.Features.Commands.CalendarCommands.DeleteCalendars;
using CoreService.Features.Queries.CalendarQueries.GetCalendars;
using CoreService.Models.Requests;
using CoreService.Models.Responses;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddCalendarsResponse))]
    public async Task<IActionResult> AddCalendars([FromBody] AddCalendarsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddCalendarsCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPut("DeleteCalendars")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteCalendarsResponse))]
    public async Task<IActionResult> DeleteCalendars([FromBody] DeleteCalendarsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteCalendarsCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpGet("Calendars")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCalendarsResponse))]
    public async Task<IActionResult> GetCalendars([FromQuery] GetCalendarsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCalendarsQuery(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }
}