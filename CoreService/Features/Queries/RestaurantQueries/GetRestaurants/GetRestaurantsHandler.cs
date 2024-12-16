using System.Text.Json;
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
            var keyword = payload.Keyword.Trim();
            var pagination = await
            (
                from res in _unitOfRepository.Restaurant.GetAll()
                join cal in _unitOfRepository.Calendar.GetAll()
                    on res.Id equals cal.RestaurantId
                where
                    cal.StartTime <= DateTime.Now
                    && cal.EndTime >= DateTime.Now
                    && res.IsActive
                    && (string.IsNullOrEmpty(keyword)
                        || EF.Functions.ILike(res.DisplayName, keyword.ToILikePattern()))
                select new GetRestaurantsData
                {
                    RestaurantId = res.Id.ToString(),
                    Avatar = res.Avatar,
                    Distance = LocationHelper.GetDistance(res.Coordinate, payload.Coordinate),
                    RestaurantName = res.DisplayName,
                    Coordinate = res.Coordinate
                }
            )
            .AsNoTracking()
            .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);
                
            var sortedData = pagination.Data.OrderBy(x => x.Distance).ToList();
            response.Data = sortedData;
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