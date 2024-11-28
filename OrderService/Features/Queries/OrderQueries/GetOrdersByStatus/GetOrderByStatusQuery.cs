using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;
using Shared.Enums;

namespace OrderService.Features.Queries.OrderQueries.GetOrdersByStatus;

public class GetOrderByStatusQuery : IRequest<GetOrderByStatusResponse> 
{
    public GetOrderByStatusRequest Payload { get; set; }
    public GetOrderByStatusQuery(GetOrderByStatusRequest payload)
    {
        Payload = payload;
    }
}