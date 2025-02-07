using MailKit.Net.Smtp;
using MimeKit;
using OpenPop.Mime.Header;

namespace Voxerra_API.Functions.Email
{
    public class EmailFunction : IEmailFunction
    {
        private const string supportEmail = "voxerraverify@gmail.com";
        private const string appPassword = "qkvw siif wcyh sxwa";

        private const string GmailSmtpServer = "smtp.gmail.com";
        private const int GmailSmtpPort = 587;

        public int GenerateCode()
        {
            Random random = new();
            return verificationCode = random.Next(10000, 99999);
        }

        public async Task SendEmail(EmailDetails emailDetails)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Voxerra support", supportEmail));
            emailMessage.To.Add(new MailboxAddress("", emailDetails.ToEmail));
            emailMessage.Subject = emailDetails.Subject;

            var bodyBuilder = new BodyBuilder();

            if (emailDetails.RegistrationEmail == true)
            {
                bodyBuilder.HtmlBody = $@"
                                <!DOCTYPE html>
                                <html lang='en'>
                                <head>
                                    <meta charset='UTF-8'>
                                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                    <title>Email Template</title>
                                    <style>
                                        body {{
                                            margin: 0;
                                            padding: 0;
                                            background-color: #f8f8f9;
                                            font-family: 'Montserrat', sans-serif;
                                        }}
                                        .container {{
                                            width: 100%;
                                            max-width: 600px;
                                            margin: auto;
                                            background-color: #ffffff;
                                            border-radius: 8px;
                                            overflow: hidden;
                                        }}
                                        .header {{
                                            background-color: #2b303a;
                                            padding: 20px;
                                            text-align: center;
                                        }}
                                        .header img {{
                                            max-width: 100%;
                                            height: auto;
                                        }}
                                        .content {{
                                            padding: 20px;
                                            text-align: center;
                                        }}
                                        .footer {{
                                            background-color: #2b303a;
                                            color: white;
                                            text-align: center;
                                            padding: 10px;
                                        }}
                                        @media (max-width: 600px) {{
                                            .container {{
                                                width: 100%;
                                            }}
                                        }}
                                        .code{{
                                            color: rgb(132, 0, 255);
                                            font-weight: 1000;
                                            font-size: 25px;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='container'>
                                        <div class='header'>
                                            <img src='cid:logo_voxerra_image' alt='Logo' />
                                        </div>
                                        <div class='content'>
                                            <h2>Verification code</h2>
                                            <p class='code'>{emailDetails.Code}</p>
                                            <p>This code will expire within 5 minutes.</p>
                                        </div>
                                        <div class='footer'>
                                            <p>Voxerra Copyright © 2025</p>
                                        </div>
                                    </div>
                                </body>
                                </html>";
            }
            else 
            {
                // spravit email template -> ked sa zmeni heslo tak sa posle tento email
                bodyBuilder.HtmlBody = $@"
                                <!DOCTYPE html>
                                <html lang='en'>
                                <head>
                                    <meta charset='UTF-8'>
                                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                    <title>Email Template</title>
                                    <style>
                                        body {{
                                            margin: 0;
                                            padding: 0;
                                            background-color: #f8f8f9;
                                            font-family: 'Montserrat', sans-serif;
                                        }}
                                        .container {{
                                            width: 100%;
                                            max-width: 600px;
                                            margin: auto;
                                            background-color: #ffffff;
                                            border-radius: 8px;
                                            overflow: hidden;
                                        }}
                                        .header {{
                                            background-color: #2b303a;
                                            padding: 20px;
                                            text-align: center;
                                        }}
                                        .header img {{
                                            max-width: 100%;
                                            height: auto;
                                        }}
                                        .content {{
                                            padding: 20px;
                                            text-align: center;
                                        }}
                                        .footer {{
                                            background-color: #2b303a;
                                            color: white;
                                            text-align: center;
                                            padding: 10px;
                                        }}
                                        @media (max-width: 600px) {{
                                            .container {{
                                                width: 100%;
                                            }}
                                        }}
                                        .code{{
                                            color: rgb(132, 0, 255);
                                            font-weight: 1000;
                                            font-size: 25px;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='container'>
                                        <div class='header'>
                                            <img src='cid:logo_voxerra_image' alt='Logo' />
                                        </div>
                                        <div class='content'>
                                            <h2>Verification code</h2>
                                            <p class='code'>{emailDetails.Code}</p>
                                            <p>This code will expire within 5 minutes.</p>
                                        </div>
                                        <div class='footer'>
                                            <p>Voxerra Copyright © 2025</p>
                                        </div>
                                    </div>
                                </body>
                                </html>";
            }

            
            var logoPath = "Resources/logo_voxerra_whitev2.png"; // Replace with the actual file path
            var logoAttachment = new MimePart("image", "png")
            {
                Content = new MimeContent(File.OpenRead(logoPath)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Inline),
                ContentTransferEncoding = (ContentEncoding)ContentTransferEncoding.Base64,
                FileName = "logo_voxerra_whitev2.png",
                Headers = { { "Content-ID", "<logo_voxerra_image>" } } // This matches the CID in the HTML content
            };

            
            //bodyBuilder.Attachments.Add(logoAttachment);
            bodyBuilder.LinkedResources.Add(logoAttachment);


            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(GmailSmtpServer, GmailSmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(supportEmail, appPassword);
                await smtpClient.SendAsync(emailMessage);
                await smtpClient.DisconnectAsync(true);
            }
        }


    }
}
