﻿using System.Text.Json;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.Helpers;
using Shared.Responses;

namespace CoreService.Features.Queries.RestaurantQueries.GetRestaurants;

public class GetRestaurantsHandler : IRequestHandler<GetRestaurantsQuery, GetRestaurantsResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<GetRestaurantsHandler> _logger;
    public GetRestaurantsHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<GetRestaurantsHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async  Task<GetRestaurantsResponse> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(GetRestaurantsHandler)} Payload = {JsonSerializer.Serialize(payload)} => ";
        var response = new GetRestaurantsResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};
        try
        {
            _logger.LogInformation(functionName);
            /* Todo: Join with calendar table to select restaurant status */
            var pagination = await _unitOfRepository.Restaurant
                .Where(res => 
                    (
                        string.IsNullOrEmpty(payload.Keyword)
                        || EF.Functions.ILike(res.DisplayName, payload.Keyword.ToILikePattern())
                    )
                    && res.IsActive
                )
                .AsNoTracking()
                .Select(res => new GetRestaurantsData
                    {
                        RestaurantId = res.Id.ToString(),
                        Avatar = res.Avatar,
                        Distance = LocationHelper.GetDistance(res.Coordinate, payload.Coordinate),
                        RestaurantName = res.DisplayName,
                        Coordinate = res.Coordinate
                    }
                )
                .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);
                
            response.Data = pagination.Data;
            response.Paging = pagination.Paging;
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = CoreServiceTranslation.EXH_ERR_01.ToString();
            response.MessageCode = CoreServiceTranslation.EXH_ERR_01.ToString();
        }

        return response;
    }
}