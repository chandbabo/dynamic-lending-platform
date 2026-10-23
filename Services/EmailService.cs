using System.Net;
using System.Net.Mail;

namespace LendingProcessingSystem.Services.Email;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string body);
}

public class SmtpEmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public SmtpEmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var host = _configuration["Smtp:Host"];
        var port = int.Parse(_configuration["Smtp:Port"]!);
        var username = _configuration["Smtp:Username"];
        var password = _configuration["Smtp:Password"];
        var fromEmail = _configuration["Smtp:From"];

        using var client = new SmtpClient(host, port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(username, password)
        };

        using var message = new MailMessage
        {
            From = new MailAddress(fromEmail!),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(email);
        await client.SendMailAsync(message);
    }
}
