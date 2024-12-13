using CoreService.Features.Commands.RestaurantCommands.UpdateRestaurantProfile;
using CoreService.Features.Queries.RestaurantQueries.GetRestaurantProfile;
using CoreService.Features.Queries.RestaurantQueries.GetRestaurants;
using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace CoreService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IMediator _mediator;
    public RestaurantController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("Profile")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRestaurantProfileResponse))]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetRestaurantProfileQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }

    [HttpPut("UpdateProfile")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateRestaurantProfileResponse))]
    public async Task<IActionResult> UpdateProfile(UpdateRestaurantProfileRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateRestaurantProfileCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPost("Restaurants")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRestaurantsResponse))]
    public async Task<IActionResult> GetRestaurantNearBy([FromBody] GetRestaurantsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetRestaurantsQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data, response.Paging);
    }
}