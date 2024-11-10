namespace CommunicationService.Models.Requests;

public class SendVerifyEmailRequest
{
    public string Email { get; set; }
    public string VerificationLink { get; set; }
}