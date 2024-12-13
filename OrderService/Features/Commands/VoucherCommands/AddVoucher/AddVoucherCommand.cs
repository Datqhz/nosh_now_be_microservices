using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.VoucherCommands.AddVoucher;

public class AddVoucherCommand : IRequest<CreateVoucherResponse>
{
    public AddVoucherRequest Payload { get; set; }
    public AddVoucherCommand(AddVoucherRequest payload)
    {
        Payload = payload;
    }
}
