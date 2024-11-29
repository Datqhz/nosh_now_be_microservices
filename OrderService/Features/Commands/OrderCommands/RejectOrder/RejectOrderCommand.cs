﻿using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.OrderCommands.RejectOrder;

public class RejectOrderCommand : IRequest<RejectOrderResponse>
{
    public int OrderId { get; set; }
    public RejectOrderCommand(int orderId)
    {
        OrderId = orderId;
    }
}