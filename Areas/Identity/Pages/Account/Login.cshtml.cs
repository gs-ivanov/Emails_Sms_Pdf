namespace Emails_Sms_Pdf.Areas.Identity.Pages.Account
{
    using global::Emails_Sms_Pdf.Data;
    using global::Emails_Sms_Pdf.Data.Models;
    using global::Emails_Sms_Pdf.Services.Sms;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly TurnirDbContext context;
        private readonly ISmsSender smsSender;




        public LoginModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            TurnirDbContext context,
            ISmsSender smsSender
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.smsSender = smsSender;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember Me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(Input.Email);

                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Грешен опит за вход.");
            return Page();
        }
    }
}