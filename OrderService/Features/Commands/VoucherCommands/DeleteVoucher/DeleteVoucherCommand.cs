using MediatR;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.VoucherCommands.DeleteVoucher;

public class DeleteVoucherCommand : IRequest<DeleteVoucherResponse>
{
    public long VoucherId { get; set; }
    public DeleteVoucherCommand(long voucherId)
    {
        VoucherId = voucherId;
    }
}
