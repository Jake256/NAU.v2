using System.Net.Mail;
using System.Net;
using NAUIdeaHub.Models;
using NAUIdeaHub.Models;

namespace NAUIdeaHub.Services
{
    public class MailService : IMailService
    {
        private readonly EmailSettings _emailConfig;
        public MailService(EmailSettings emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(string RecipientAddress, string Subject, string HTMLBody)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_emailConfig.SenderAddress);
            message.To.Add(new MailAddress(RecipientAddress));
            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = HTMLBody;
            smtp.Port = _emailConfig.Port;
            smtp.Host = _emailConfig.EmailServerName;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            //smtp.Credentials = new NetworkCredential(_emailConfig.Username, _emailConfig.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
        }
    }
}
