using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using Gestion_RDV.Models.Repository;
using Gestion_RDV.Models.DTO;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(EmailDTO email)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("halie.ullrich@ethereal.email"));
            message.To.Add(MailboxAddress.Parse(email.To));
            message.Subject = email.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = email.Body };

            var smtpSettings = _configuration.GetSection("Smtp");

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpSettings.GetValue<string>("Host"),
                                smtpSettings.GetValue<int>("Port"),
                                SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(smtpSettings.GetValue<string>("UserName"), 
                                    smtpSettings.GetValue<string>("Password"));

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to send email: {ex.Message}", ex);
        }
    }
}