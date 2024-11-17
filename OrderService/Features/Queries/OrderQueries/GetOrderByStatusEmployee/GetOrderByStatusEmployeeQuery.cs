using MediatR;
using OrderService.Models.Responses;
using Shared.Enums;

namespace OrderService.Features.Queries.OrderQueries.GetOrderByStatusEmployee;

public class GetOrderByStatusEmployeeQuery : IRequest<GetOrderByStatusEmployeeResponse>
{
    public OrderStatus OrderStatus { get; set; }
    public GetOrderByStatusEmployeeQuery(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}