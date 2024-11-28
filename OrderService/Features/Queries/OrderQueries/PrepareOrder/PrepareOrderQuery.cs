using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Queries.OrderQueries.PrepareOrder;

public class PrepareOrderQuery : IRequest<PrepareOrderResponse>
{
    public long OrderId { get; set; }
    public PrepareOrderQuery(long orderId)
    {
        OrderId = orderId;
    }
}