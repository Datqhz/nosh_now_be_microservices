using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Features.Commands.CategoryCommands.AddCategory;
using OrderService.Features.Commands.CategoryCommands.UpdateCategory;
using OrderService.Features.Queries.CategoryQueries.GetCategories;
using OrderService.Models.Requests;
using OrderService.Models.Responses;
using Shared.Responses;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoryController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("Categories")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoriesResponse))]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCategoriesQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateCategoryResponse))]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddCategoryCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateCategoryResponse))]
    public async Task<IActionResult>UpdateCategory([FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateCategoryCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
}