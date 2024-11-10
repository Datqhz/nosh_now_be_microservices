namespace CommunicationService.Services;

public interface IEmailService
{
    Task SendVerifyEmail(string toEmail, string verificationToken, string userName);
}