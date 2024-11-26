using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationService.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
}