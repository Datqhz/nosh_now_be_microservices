using CoreService.Features.Commands.RestaurantCommands.UpdateRestaurantProfile;
using CoreService.Features.Queries.EmployeeQueries.GetEmployeeById;
using CoreService.Features.Queries.EmployeeQueries.GetEmployees;
using CoreService.Features.Queries.RestaurantQueries.GetRestaurantProfile;
using CoreService.Features.Queries.RestaurantQueries.GetRestaurants;
using CoreService.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace CoreService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeeController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("{employeeId}/Profile")]
    public async Task<IActionResult> GetProfile([FromRoute] string employeeId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }

    /*[HttpPut("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile(UpdateRestaurantProfileRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateRestaurantProfileCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }*/
    
    [HttpPost("Employees")]
    public async Task<IActionResult> GetRestaurantNearBy([FromBody] GetEmployeesRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmployeesQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data, response.Paging);
    }
}
