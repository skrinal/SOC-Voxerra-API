using Google.Apis.Gmail.v1;
using MailKit.Net.Smtp;
using MimeKit;

namespace Voxerra_API.Helpers
{
    public class EmailMessage
    {
        private const string supportEmail = "voxerraverify@gmail.com";
        private const string appPassword = "qkvw siif wcyh sxwa";

        private const string GmailSmtpServer = "smtp.gmail.com";
        private const int GmailSmtpPort = 587;
        
        public async Task<bool> SendEmail(string toEmail, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Voxerra support", supportEmail));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = body
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(GmailSmtpServer, GmailSmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(supportEmail, appPassword);
                await smtpClient.SendAsync(emailMessage);
                await smtpClient.DisconnectAsync(true);
            }
            return true;
        }


    }
}
