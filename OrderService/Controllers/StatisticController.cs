﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Features.Queries.RestaurantQueries.GetOverview;
using OrderService.Features.Queries.StatisticQueries.RestaurantStatistic;
using OrderService.Models.Requests;
using OrderService.Models.Responses;
using Shared.Responses;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StatisticController : ControllerBase
{
    private readonly IMediator _mediator;
    public StatisticController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("GetStatistics")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CalculateRestaurantStatisticResponse))]
    public async Task<IActionResult> GetStatistics([FromQuery] CalculateRestaurantStatisticRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CalculateRestaurantStatisticQuery(request), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode,
            response.Data);
    }
    
    [HttpGet("Overview")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRestaurantOverviewResponse))]
    public async Task<IActionResult> GetFoodsByRestaurant([FromQuery] DateTime date, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOverviewQuery(date), cancellationToken);
        return ResponseHelper.ToResponse(response.StatusCode, response.ErrorMessage, response.MessageCode, response.Data);
    }
}