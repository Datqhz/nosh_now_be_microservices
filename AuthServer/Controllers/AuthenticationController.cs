﻿using AuthServer.Features.Commands.AccountCommands.ConfirmVerificationEmail;
using AuthServer.Features.Commands.AccountCommands.Login;
using AuthServer.Features.Commands.AccountCommands.Register;
using AuthServer.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace AuthServer.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RegisterCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(result.StatusCode, result.ErrorMessage, result.ErrorMessage);
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> GetToken([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LoginCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(result.StatusCode, result.ErrorMessage, result.ErrorMessage, result.Data);
    }
    
    [HttpGet("VerifyEmail")]
    public async Task<IActionResult> GetToken([FromQuery] ConfirmVerificationEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ConfirmVerificationEmailCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(result.StatusCode, result.ErrorMessage, result.ErrorMessage);
    }
    
    [Authorize]
    [HttpGet("test")]
    public async Task<IActionResult> GetTokenTest([FromQuery] string test)
    {
        var Dot1 = test.IndexOf('.');
        if (Dot1 == -1 || Dot1 == test.Length - 1)
            return Ok();
        var Dot2 = test.IndexOf('.', Dot1 + 1);
        if (Dot2 == -1)
            return Ok();
        var Dot3 = Dot2 != test.Length - 1 ? test.IndexOf('.', Dot2 + 1) : -1;
        return Ok();
    }
}