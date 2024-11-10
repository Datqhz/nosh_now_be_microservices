namespace AuthServer.Models.Requests;

public class ConfirmVerificationEmailRequest
{
    public string Email { get; set; }
    public string VerificationToken { get; set; }
}