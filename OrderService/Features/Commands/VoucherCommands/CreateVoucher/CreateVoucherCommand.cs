using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.VoucherCommands.CreateVoucher;

public class CreateVoucherCommand : IRequest<CreateVoucherResponse>
{
    public CreateVoucherRequest Payload { get; set; }
    public CreateVoucherCommand(CreateVoucherRequest payload)
    {
        Payload = payload;
    }
}
