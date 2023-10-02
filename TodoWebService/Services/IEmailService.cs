namespace TodoWebService.Services;

public interface IEmailService
{
    void SendEmail(string toEmail, string subject, string message);
}
