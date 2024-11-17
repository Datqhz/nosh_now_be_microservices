using CoreService.Features.Commands.CustomerCommands.UpdateCustomerProfile;
using CoreService.Features.Queries.CustomerQueries;
using CoreService.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace CoreService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("Profile")]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCustomerProfileQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }

    [HttpPut("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile(UpdateCustomerProfileRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateCustomerProfileCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode);
    }
    
}