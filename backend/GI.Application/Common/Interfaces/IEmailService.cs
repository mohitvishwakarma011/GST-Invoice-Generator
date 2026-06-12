namespace GI.Application.Common.Interfaces
{
    public interface IEmailService
    {
        void SendEmailAsync(string toEmail,
            string subject,
            string htmlBody,
            List<byte[]>? filesBytes);
    }
}
