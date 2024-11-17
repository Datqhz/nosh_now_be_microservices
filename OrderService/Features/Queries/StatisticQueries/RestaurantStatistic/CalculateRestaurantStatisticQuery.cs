using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Queries.StatisticQueries.RestaurantStatistic;

public class CalculateRestaurantStatisticQuery : IRequest<CalculateRestaurantStatisticResponse>
{
    public CalculateRestaurantStatisticRequest Payload { get; set; }
    public CalculateRestaurantStatisticQuery(CalculateRestaurantStatisticRequest payload)
    {
        Payload = payload;
    }
}