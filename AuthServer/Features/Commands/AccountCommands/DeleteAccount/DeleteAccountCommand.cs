using AuthServer.Models.Responses;
using MediatR;

namespace AuthServer.Features.Commands.AccountCommands.DeleteAccount;

public class DeleteAccountCommand : IRequest<DeleteAccountResponse>
{
    public string AccountId { get; set; }

    public DeleteAccountCommand(string accountId)
    {
        AccountId = accountId;
    }
}
