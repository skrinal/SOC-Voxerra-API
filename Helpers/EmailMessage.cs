//using MailKit.Net.Smtp;
//using MailKit;
//using MimeKit;

//namespace Voxerra_API.Helpers
//{
//    public class EmailMessage
//    {
//        private readonly string supportEmail = "voxerraverify@gmail.com";

//        public void SendEmail(string emailTo, int code, string userName)
//        {
//            var email = new MimeMessage();

//            email.From.Add(new MailboxAddress("Voxerra Verify", supportEmail));
//            email.To.Add(new MailboxAddress(userName, emailTo));

//            email.Subject = "Testing out email sending";
//            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
//            {
//                Text = "<b>Hello all the way from the land of C#</b>"
//            };

//            using (var smtp = new SmtpClient())
//            {
//                smtp.Connect("smtp.gmail.com", 587, false);

//                // Note: only needed if the SMTP server requires authentication
//                smtp.Authenticate(supportEmail, "IAaod[]\\;13fffGG");

//                smtp.Send(email);
//                smtp.Disconnect(true);
//            }
//        }
        
//    }
//}
