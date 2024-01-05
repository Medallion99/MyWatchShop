using MailKit.Net.Smtp;
using MimeKit;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration configuration)
        {
            this._config = configuration;
        }

        public async Task<string> SendEmailAsync(string emailRecipient, string subject, string body)
        {
            try
            {
                var senderEmail = _config.GetSection("EmailSettings:sender").Value;
                var port = Convert.ToInt32(_config.GetSection("EmailSettings:port").Value);
                var host = _config.GetSection("EmailSettings:host").Value;
                var appPassword = _config.GetSection("EmailSettings:appPassword").Value;

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(senderEmail);
                email.To.Add(MailboxAddress.Parse(emailRecipient));
                email.Subject = subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    smtp.CheckCertificateRevocation = true;
                    await smtp.ConnectAsync(host, port, true);
                    await smtp.AuthenticateAsync(senderEmail, appPassword);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }
                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
