using System.Net.Mail;
using System.Net;

namespace TodoWebService.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(string toEmail, string subject, string message)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");

        var smtpClient = new SmtpClient
        {
            Host = smtpSettings["Host"],
            Port = int.Parse(smtpSettings["Port"]),
            EnableSsl = bool.Parse(smtpSettings["EnableSsl"]),
            Credentials = new NetworkCredential(
                smtpSettings["Username"],
                smtpSettings["Password"]
            )
        };

        using var Message = new MailMessage
        {
            From = new MailAddress(smtpSettings["Username"]),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        Message.To.Add(toEmail);

        smtpClient.Send(Message);
    }
}
