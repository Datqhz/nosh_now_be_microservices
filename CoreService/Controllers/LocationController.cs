using CoreService.Features.Commands.EmployeeCommands.UpdateEmployeeProfile;
using CoreService.Features.Commands.LocationCommands.CreateLocation;
using CoreService.Features.Commands.LocationCommands.DeleteLocation;
using CoreService.Features.Commands.LocationCommands.UpdateLocation;
using CoreService.Features.Queries.RestaurantQueries.GetRestaurants;
using CoreService.Features.Queries.SavedLocationQueries.GetSavedLocationByCustomer;
using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace CoreService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IMediator _mediator;
    public LocationController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("SavedLocations")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSavedLocationByCustomerResponse))]
    public async Task<IActionResult> GetSavedLocations(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetSavedLocationByCustomerQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }
    
    [HttpPost("Saved")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateLocationResponse))]
    public async Task<IActionResult> SaveNewLocation([FromBody] CreateLocationRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateLocationCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }

    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateLocationResponse))]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateLocationRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateLocationCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateLocationResponse))]
    public async Task<IActionResult> UpdateProfile([FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteLocationCommand(id), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
}