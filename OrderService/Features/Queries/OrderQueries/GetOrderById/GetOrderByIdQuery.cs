using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Queries.OrderQueries.GetOrderById;

public class GetOrderByIdQuery : IRequest<GetOrderByIdResponse>
{
    public long OrderId { get; set; }
    public GetOrderByIdQuery(long orderId)
    {
        OrderId = orderId;
    }
}