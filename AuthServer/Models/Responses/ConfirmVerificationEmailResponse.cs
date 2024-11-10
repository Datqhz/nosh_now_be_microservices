using Shared.Responses;

namespace AuthServer.Models.Responses;

public class ConfirmVerificationEmailResponse : BaseResponse
{
    public string AccountId { get; set; }
}