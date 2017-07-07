using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Net;
using System.Threading.Tasks;
using static HB.Models.Email;
using static HB.Services.IEmailServices;

namespace HB.Services
{
    public class EmailServices
    {
        public class EmailService : IEmailService
        {
            private readonly EmailConfig ec;

            public EmailService(IOptions<EmailConfig> emailConfig)
            {
                this.ec = new EmailConfig
                {
                    FromName = "Hanna",
                    FromAddress = "hlbernard124@gmail.com",

                    LocalDomain = "hann.life",

                    MailServerAddress = "in-v3.mailjet.com",
                    MailServerPort = "587",

                    UserId = "7c56da0c21b0ff111d8a0a9862fab920",
                    UserPassword = "4470d916b1413025e444d1beb03baf8b"
                };
            }

            public async Task SendEmailAsync(String email, String subject, String message)
            {
                try
                {
                    var emailMessage = new MimeMessage();

                    emailMessage.From.Add(new MailboxAddress("Hanna", "hlbernard124@gmail.com"));
                    emailMessage.To.Add(new MailboxAddress("Hanna", "hlbernard124@gmail.com"));
                    emailMessage.Subject = subject;
                    emailMessage.Body = new TextPart(TextFormat.Html) { Text = $"{email} send a message to you at {DateTime.Now}... message = {message}" };

                    using (var client = new SmtpClient())
                    {
                        client.LocalDomain = "hann.life";

                        await client.ConnectAsync("in-v3.mailjet.com", Convert.ToInt32("587"), SecureSocketOptions.Auto).ConfigureAwait(false);
                        await client.AuthenticateAsync(new NetworkCredential("7c56da0c21b0ff111d8a0a9862fab920", "4470d916b1413025e444d1beb03baf8b"));
                        await client.SendAsync(emailMessage).ConfigureAwait(false);
                        await client.DisconnectAsync(true).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
