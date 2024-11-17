using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Features.Commands.CategoryCommands.CreateCategory;
using OrderService.Features.Commands.CategoryCommands.UpdateCategory;
using OrderService.Features.Queries.CategoryQueries.GetCategories;
using OrderService.Models.Requests;
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
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCategoriesQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateCategoryCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPut]
    public async Task<IActionResult>UpdateCategory([FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateCategoryCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
}