namespace Emails_Sms_Pdf.Controllers
{
    using Emails_Sms_Pdf.Data;
    using Emails_Sms_Pdf.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly TurnirDbContext _context;

        public HomeController(TurnirDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IActionResult> Index()
        {
            // Hhhhhhhhhhh
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
