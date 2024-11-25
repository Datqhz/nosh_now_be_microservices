using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderDetailCommands.DeleteOrderDetail;

public class DeleteOrderDetailCommand : IRequest<DeleteOrderDetailResponse>
{
    public long OrderDetailId { get; set; }
    public DeleteOrderDetailCommand(long orderDetailId)
    {
        OrderDetailId = orderDetailId;
    }
}