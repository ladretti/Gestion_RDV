using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Gestion_RDV.Email;
using Microsoft.Extensions.Configuration;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var smtpConfig = _configuration.GetSection("Smtp");

        using (var client = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"])))
        {
            client.Credentials = new NetworkCredential(smtpConfig["UserName"], smtpConfig["Password"]);
            client.EnableSsl = bool.Parse(smtpConfig["EnableSsl"]);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpConfig["UserName"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
        }
    }
}