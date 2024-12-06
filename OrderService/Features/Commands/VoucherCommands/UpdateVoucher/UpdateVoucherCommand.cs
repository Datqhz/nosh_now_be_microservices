using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.VoucherCommands.UpdateVoucher;

public class UpdateVoucherCommand : IRequest<UpdateVoucherResponse>
{
    public UpdateVoucherRequest Payload { get; set; }
    public UpdateVoucherCommand(UpdateVoucherRequest payload)
    {
        Payload = payload;
    }
}
