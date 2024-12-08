using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Queries.RestaurantQueries.GetOverview;

public class GetOverviewQuery : IRequest<GetRestaurantOverviewResponse>
{
    public DateTime CurrentDate { get; set; }
    public GetOverviewQuery(DateTime currentDate)
    {
        CurrentDate = currentDate;
    }
}