using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.Controllers;

[ApiController]
[Route("[controller]")]
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
    
}