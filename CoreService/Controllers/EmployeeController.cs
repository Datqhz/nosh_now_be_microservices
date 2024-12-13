using CoreService.Features.Commands.EmployeeCommands.DeleteEmployee;
using CoreService.Features.Commands.EmployeeCommands.UpdateEmployeeProfile;
using CoreService.Features.Queries.EmployeeQueries.GetEmployeeById;
using CoreService.Features.Queries.EmployeeQueries.GetEmployeeProfile;
using CoreService.Features.Queries.EmployeeQueries.GetEmployees;
using CoreService.Models.Requests;
using CoreService.Models.Responses;
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

    [HttpGet("{employeeId}/Information")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetEmployeeByIdResponse))]
    public async Task<IActionResult> GetInformation([FromRoute] string employeeId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }
    
    [HttpGet("Profile")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetEmployeeProfileResponse))]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmployeeProfileQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteEmployeeResponse))]
    public async Task<IActionResult> DeleteEmployee(string id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteEmployeeCommand(id), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPost("Employees")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetEmployeesResponse))]
    public async Task<IActionResult> GetEmployees([FromBody] GetEmployeesRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmployeesQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data, response.Paging);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateEmployeeProfileResponse))]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeProfileRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateEmployeeProfileCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
}
