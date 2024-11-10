using System.Net;
using System.Net.Mail;
using System.Web;
using CommunicationService.Constants;
using CommunicationService.Resources;

namespace CommunicationService.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService
    (
        ILogger<EmailService> logger
    )
    {
        _logger = logger;
    }
    public Task SendVerifyEmail(string toEmail, string verificationToken, string userName)
    {
        var functionName = $"{nameof(EmailService)} - {nameof(SendVerifyEmail)} => ";
        try
        {
            _logger.LogInformation(functionName);
            var body = ProcessVerificationEmailBody(verificationToken, userName, toEmail);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = EmailConstant.SMTPPort,
                Credentials = new NetworkCredential(EmailConstant.SMTPUser, EmailConstant.SMTPPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(EmailConstant.SMTPUser, EmailConstant.CompanyName),
                Subject = string.Concat(EmailConstant.CompanyName, EmailSubject.VerificationEmail),
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }
        return Task.CompletedTask;
    }

    #region Private methods

    private string ProcessVerificationEmailBody(string verificationToken, string userName, string email)
    {
        var template = EmailTemplates.VerificationEmailTemplate;
        string tokenSafeString = HttpUtility.UrlEncode(verificationToken);
        string emailSafeString = HttpUtility.UrlEncode(email);
        var verificationLink =  $"http://localhost:5237/api/v1/Authentication/VerifyEmail?Email={emailSafeString}&VerificationToken={tokenSafeString}";
        template = template.Replace("{{VerificationLink}}", verificationLink);
        template = template.Replace("{{UserName}}", userName);
        return template;
    }

    #endregion
}