using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using HB.Services;
using HB.Models;

namespace HB.Controllers
{
    public class HomeController : Controller
    {
        private IEmailSender _emailSender { get; }
        public HomeController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult Contact(Contact contact)
        {
            ViewData["Message"] = null;
            ViewData["Error"] = null;

            if (ModelState.IsValid)
            {      
                var email = contact.Email;
                var message = contact.Message;

                _emailSender.SendEmailAsync(email, message);

                ViewData["message_color"] = "green";
                ViewData["Message"] = "*Your Message Has Been Sent.";
            }
            else
            {
                ViewData["message_color"] = "red";
                ViewData["Message"] = "*Please complete the required fields";
            }

            ModelState.Clear();
            return RedirectToAction("Index","Home");
        }

        public IActionResult Index()
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
