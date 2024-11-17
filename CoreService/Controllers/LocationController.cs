using CoreService.Features.Commands.CustomerCommands.UpdateCustomerProfile;
using CoreService.Features.Commands.LocationCommands.CreateLocation;
using CoreService.Features.Commands.LocationCommands.UpdateLocation;
using CoreService.Features.Queries.RestaurantQueries.GetRestaurants;
using CoreService.Features.Queries.SavedLocationQueries.GetSavedLocationByCustomer;
using CoreService.Models.Requests;
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
    public async Task<IActionResult> GetSavedLocations(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetSavedLocationByCustomerQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }
    
    [HttpPost("Saved")]
    public async Task<IActionResult> SaveNewLocation([FromBody] CreateLocationRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateLocationCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateLocationRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateLocationCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPost("Restaurants")]
    public async Task<IActionResult> GetRestaurantNearBy([FromBody] GetRestaurantsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetRestaurantsQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data, response.Paging);
    }
}