﻿using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Enums;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Queries.StatisticQueries.RestaurantStatistic;

public class CalculateRestaurantStatisticHandler : IRequestHandler<CalculateRestaurantStatisticQuery, CalculateRestaurantStatisticResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CalculateRestaurantStatisticHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public CalculateRestaurantStatisticHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CalculateRestaurantStatisticHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CalculateRestaurantStatisticResponse> Handle(CalculateRestaurantStatisticQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(CalculateRestaurantStatisticHandler)} =>";
        var response = new CalculateRestaurantStatisticResponse {StatusCode = (int)ResponseStatusCode.InternalServerError};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);
            var orderIds = await _unitOfRepository.Order
                .Where(x => x.RestaurantId.Equals(currentUserId) && x.Status == OrderStatus.Success)
                .AsNoTracking()
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            if (!orderIds.Any())
            {
                response.StatusCode = (int)ResponseStatusCode.Ok;
                return response;
            }

            response.Data.TotalOrder = orderIds.Count();
            var totalRevenue = await _unitOfRepository.OrderDetail
                .Where(x => orderIds.Contains(x.OrderId))
                .AsNoTracking()
                .SumAsync(x => x.Amount * x.Price, cancellationToken);
            response.Data.TotalRevenue = totalRevenue;

            var topFoods = await
                (
                    from od in _unitOfRepository.OrderDetail.GetAll()
                    join f in _unitOfRepository.Food.GetAll()
                        on od.FoodId equals f.Id
                    where orderIds.Contains(od.OrderId)
                    group new { od, f } by new { f.Id, f.Name }
                    into grouped
                    select new TopFoodData
                    {
                        FoodName = grouped.Key.Name,
                        TotalRevenue = grouped.Sum(x => x.od.Amount * x.od.Price)
                    }
                )
                .Skip(0)
                .Take(5)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            response.Data.TopFoods = topFoods;
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            ex.LogError(functionName, _logger);
        }

        return response;
    }
}