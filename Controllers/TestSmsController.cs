namespace Emails_Sms_Pdf.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Emails_Sms_Pdf.Services.Sms;

    public class TestSmsController : Controller
    {
        private readonly ISmsSender _smsSender;

        public TestSmsController(ISmsSender smsSender)
        {
            _smsSender = smsSender;
        }

        public async Task<IActionResult> Send()
        {
            var to = "+359894666859"; // или друг твой номер
            var message = "⚽ 30.04.2025 10:59 Test SMS от Emails_Sms_Pdf чрез Twilio. Confirm, pls!";

            //await _smsSender.SendSmsAsync(to, message);

            return Content("✅ SMS изпратен успешно виртуално! Коментиран код: await _smsSender.SendSmsAsync(to, message); в public TestSmsController(ISmsSender smsSender)");
        }
    }
}
