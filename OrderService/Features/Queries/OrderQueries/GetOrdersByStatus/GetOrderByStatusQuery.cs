using MediatR;
using OrderService.Models.Responses;
using Shared.Enums;

namespace OrderService.Features.Queries.OrderQueries.GetOrdersByStatus;

public class GetOrderByStatusQuery : IRequest<GetOrderByStatusResponse> 
{
    public OrderStatus OrderStatus { get; set; }
    public GetOrderByStatusQuery(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}