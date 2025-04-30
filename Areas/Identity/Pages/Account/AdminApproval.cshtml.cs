using Emails_Sms_Pdf.Data; // или TurnirDbContext
using Emails_Sms_Pdf.Services.Sms; // ако имаш ISmsSender
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;


[Authorize(Roles = "Admin")]
public class AdminApprovalModel : PageModel
{
    private readonly TurnirDbContext _context;
    private readonly ISmsSender _smsSender;

    public AdminApprovalModel(TurnirDbContext context, ISmsSender smsSender)
    {
        _context = context;
        _smsSender = smsSender;
    }

    [BindProperty(SupportsGet = true)]
    public string UserId { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        return RedirectToPage("/Index");
    }
}
