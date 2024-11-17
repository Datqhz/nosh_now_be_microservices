using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Features.Commands.IngredientCommands.CreateIngredient;
using OrderService.Features.Commands.IngredientCommands.DeleteIngredient;
using OrderService.Features.Commands.IngredientCommands.UpdateIngredient;
using OrderService.Features.Queries.IngredientQueries.GetIngredients;
using OrderService.Models.Requests;
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
    public async Task<IActionResult> GetIngredients(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetIngredientsQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AddIngredient([FromBody] CreateIngredientRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateIngredientCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPut]
    public async Task<IActionResult>UpdateIngredient([FromBody] UpdateIngredientRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateIngredientCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpDelete("{ingredientId}")]
    public async Task<IActionResult> DeleteIngredient(int ingredientId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteIngredientCommand(ingredientId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
}