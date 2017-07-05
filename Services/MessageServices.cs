using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Services
{
    public class MessageServices
    {
        public class AuthMessageSender : IEmailSender
        {
            public async Task SendEmailAsync(string email, string message)
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("TEST", "Hlbernard124@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Body = new TextPart("plain") { Text = message };

                await Task.Run(() =>
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smpt.gmail.com", 587, false);

                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        client.Send(emailMessage);
                        client.Disconnect(true);
                    }
                });
            }
        }
    }
}

