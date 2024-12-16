using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Features.Commands.IngredientCommands.AddIngredient;
using OrderService.Features.Commands.IngredientCommands.DeleteIngredient;
using OrderService.Features.Commands.IngredientCommands.UpdateIngredient;
using OrderService.Features.Queries.IngredientQueries.GetIngredients;
using OrderService.Models.Requests;
using OrderService.Models.Responses;
using Shared.Responses;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class IngredientController : ControllerBase
{
    private readonly IMediator _mediator;
    public IngredientController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpGet("Ingredients")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetIngredientsResponse))]
    public async Task<IActionResult> GetIngredients(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetIngredientsQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateIngredientResponse))]
    public async Task<IActionResult> AddIngredient([FromBody] AddIngredientRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddIngredientCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateIngredientResponse))]
    public async Task<IActionResult>UpdateIngredient([FromBody] UpdateIngredientRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateIngredientCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpDelete("{ingredientId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteIngredientResponse))]
    public async Task<IActionResult> DeleteIngredient(int ingredientId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteIngredientCommand(ingredientId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
}