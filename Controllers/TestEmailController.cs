namespace Emails_Sms_Pdf.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Emails_Sms_Pdf.Services.Email;
    using System.Threading.Tasks;

    public class TestEmailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public TestEmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Send()
        {
            //var to = "ivanovkoko2006@gmail.com"; // замени с твоя адрес
            var to = "gs.ivanov50@gmail.com"; // замени с твоя адрес
            var subject = "⚽ Тестване на Email от Emails_Sms_Pdf";
            var body = "30.04.2025 10:00 Това е тестово известие от приложението Emails_Sms_Pdf.\nУспешно сме свързали Gmail SMTP.\nConfirm";

            //await _emailSender.SendAsync(to, subject, body);

            TempData["Email"] = "✅ Виртуално изпращане на Емайл успешно! //await client.SendMailAsync(message);";

            return Content("✅ Виртуално изпращане на Емайл успешно! //await client.SendMailAsync(message);!");
        }

        public IActionResult Test() => View();
    }
}
