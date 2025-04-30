namespace Emails_Sms_Pdf.Areas.Identity.Pages.Account
{
    using Emails_Sms_Pdf.Data;
    using Emails_Sms_Pdf.Data.Models;
    using Emails_Sms_Pdf.Services.Sms;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;



    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly TurnirDbContext context;
        private readonly ISmsSender smsSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            TurnirDbContext context,
            ISmsSender smsSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.smsSender = smsSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Имейл")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърди парола")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
            public string ConfirmPassword { get; set; }

            [Phone]
            [Display(Name = "Телефонен номер")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Пълно име")]
            public string FullName { get; set; }

            [Display(Name = "Стани мениджър")]
            public bool BecomeManager { get; set; }

            [Display(Name = "Избери отбор")]
            public int? TeamId { get; set; }

            public SelectList AvailableTeams { get; set; }
        }

        public SelectList AvailableTeams { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            //var smsText = $"✅ Заявката ви за участие в турнир \"Всеки срещу всеки\" е приета.\nСлед превод по IBAN: BG00XXXX00000000000000, въведете имейл {user.Email} във формата за потвърждение.";
            //var phone = user.PhoneNumber ?? "+359885773102";
            //await smsSender.SendSmsAsync(phone, smsText);
            //TempData["Message"] = $"Изпратен СМС на телефонен нномер {phone}.";
            return RedirectToPage("/Index");
        }
    }
}
