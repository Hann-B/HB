using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HB.Models;
using static HB.Services.IEmailServices;

namespace HB.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
