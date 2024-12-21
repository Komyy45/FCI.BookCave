using FCI.BookCave.Dashboard.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FCI.BookCave.Dashboard.Controllers
{
    public class EmailController : Controller
    {
        private IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<ActionResult> sendEmail()
        {
            return View("sendEmail");
        }

        [HttpPost]
        public async Task <ActionResult> sendEmail(EmailViewModel email)
        {
            await _emailService.sendAsync("ahmedhisham8.ah@gmail.com","Rawash BYMSI","Helloz Afify");
            return RedirectToAction("Index", "Home");
        }
    }
}
