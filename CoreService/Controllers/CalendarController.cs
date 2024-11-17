using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CalendarController : ControllerBase
{
    private readonly IMediator _mediator;
    public CalendarController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    
}