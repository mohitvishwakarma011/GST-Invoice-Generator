using GI.Application.Common;
using GI.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GI.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSetting;
        public EmailService(IOptions<EmailSettings> emailSetting) 
        {
            _emailSetting = emailSetting.Value;
        }

        public async void SendEmailAsync(string toEmail, string subject, string htmlBody, List<byte[]>? filesBytes)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(
                _emailSetting.FromName,
                _emailSetting.Username));

            email.To.Add(MailboxAddress.Parse(toEmail));

            email.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };

            if(filesBytes is not null)
            {
                foreach (var file in filesBytes)
                {
                    builder.Attachments.Add(
                    "Invoice.pdf",
                    file,
                    ContentType.Parse("application/pdf"));
                }
            }

            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            await smtp.ConnectAsync(
                _emailSetting.Host,
                587,
                MailKit.Security.SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _emailSetting.Username,
                _emailSetting.Password);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
