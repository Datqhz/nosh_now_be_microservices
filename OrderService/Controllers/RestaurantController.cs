using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Features.Queries.RestaurantQueries.GetRestaurants;
using OrderService.Models.Requests;
using OrderService.Models.Responses;
using Shared.Responses;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RestaurantController: ControllerBase
{
    private readonly IMediator _mediator;
    public RestaurantController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpPost("ByCategory")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRestaurantsByCategoryResponse))]
    public async Task<IActionResult> GetFoodsByRestaurant([FromBody] GetRestaurantsByCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetRestaurantsByCategoryQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data, response.Paging);
    }
}