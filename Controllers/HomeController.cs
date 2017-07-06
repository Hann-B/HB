using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using HB.Models;
using Microsoft.Extensions.Options;
using static HB.Models.Email;
using MimeKit.Text;
using System.Net;
using MailKit.Security;

namespace HB.Controllers
{
    public class HomeController : Controller
    {
        //Interface and implementing class
        public interface IEmailService
        {
            Task SendEmailAsync(string email, string subject, string message);
        }

        public class EmailService : IEmailService
        {
            private readonly EmailConfig ec;

            public EmailService(IOptions<EmailConfig> emailConfig)
            {
                this.ec = new EmailConfig
                {
                    FromName="Hanna",
                    FromAddress="hlbernard124@gmail.com",
                    
                    LocalDomain= "hann.life",

                    MailServerAddress= "in-v3.mailjet.com",
                    MailServerPort="587",

                    UserId= "7c56da0c21b0ff111d8a0a9862fab920",
                    UserPassword= "4470d916b1413025e444d1beb03baf8b"
                };
            }

            public async Task SendEmailAsync(String email, String subject, String message)
            {
                try
                {
                    var emailMessage = new MimeMessage();

                    emailMessage.From.Add(new MailboxAddress(ec.FromName, ec.FromAddress));
                    emailMessage.To.Add(new MailboxAddress(ec.FromName, ec.FromAddress));
                    emailMessage.Subject = subject;
                    emailMessage.Body = new TextPart(TextFormat.Html) { Text = $"{email} send a message to you at {DateTime.Now}... message = {message}" };

                    using (var client = new SmtpClient())
                    {
                        client.LocalDomain = ec.LocalDomain;

                        await client.ConnectAsync(ec.MailServerAddress, Convert.ToInt32(ec.MailServerPort), SecureSocketOptions.Auto).ConfigureAwait(false);
                        await client.AuthenticateAsync(new NetworkCredential(ec.UserId, ec.UserPassword));
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

        private readonly IEmailService _emailService;

        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost()]
        public async Task<IActionResult> Email(MailViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailService.SendEmailAsync(model.MailTo, model.Subject, model.Message);

                ViewBag.Succes = true;
            }

            return PartialView(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Resume()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
