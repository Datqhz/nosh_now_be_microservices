
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Features.Commands;
using OrderService.Features.Commands.CategoryCommands.UpdateCategory;
using OrderService.Features.Commands.FoodCommands.AddFood;
using OrderService.Features.Commands.FoodCommands.DeleteFood;
using OrderService.Features.Commands.FoodCommands.UpdateFood;
using OrderService.Features.Queries.FoodQueries.GetFoodById;
using OrderService.Features.Queries.FoodQueries.GetFoods;
using OrderService.Features.Queries.FoodQueries.GetFoodsByRestaurant;
using OrderService.Models.Requests;
using Shared.Responses;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FoodController : BaseResponse
{
    private readonly IMediator _mediator;
    public FoodController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpGet("Foods/{restaurantId}")]
    public async Task<IActionResult> GetFoodsByRestaurant(string restaurantId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetFoodsByRestaurantQuery(restaurantId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }
    
    [HttpGet("Foods")]
    public async Task<IActionResult> GetFoodsByRestaurant([FromQuery] GetFoodsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetFoodsQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data, response.Paging);
    }
    
    [HttpGet("{foodId}")]
    public async Task<IActionResult> GetFoodById(int foodId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetFoodByIdQuery(foodId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AddFood([FromBody] AddFoodRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddFoodCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }
    
    [HttpPut]
    public async Task<IActionResult>UpdateFood([FromBody] UpdateFoodRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateFoodCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpDelete("{foodId}")]
    public async Task<IActionResult> DeleteFood(int foodId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteFoodCommand(foodId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpGet("dump")]
    public async Task GetDump(CancellationToken cancellationToken)
    {
        await _mediator.Send(new DumpDataCommand(), cancellationToken);
    }
}