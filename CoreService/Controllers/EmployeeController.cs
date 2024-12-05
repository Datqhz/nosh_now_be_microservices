using CoreService.Features.Commands.EmployeeCommands.DeleteEmployee;
using CoreService.Features.Commands.EmployeeCommands.UpdateEmployeeProfile;
using CoreService.Features.Queries.EmployeeQueries.GetEmployeeById;
using CoreService.Features.Queries.EmployeeQueries.GetEmployeeProfile;
using CoreService.Features.Queries.EmployeeQueries.GetEmployees;
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

    [HttpGet("{employeeId}/Information")]
    public async Task<IActionResult> GetInformation([FromRoute] string employeeId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }
    
    [HttpGet("Profile")]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmployeeProfileQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(string id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteEmployeeCommand(id), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
    [HttpPost("Employees")]
    public async Task<IActionResult> GetEmployees([FromBody] GetEmployeesRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetEmployeesQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data, response.Paging);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeProfileRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateEmployeeProfileCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
}
