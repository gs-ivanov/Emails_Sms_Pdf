namespace Emails_Sms_Pdf.Infrastructure.Extensions
{
    using System.Security.Claims;

    using static Areas.Admin.AdminConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
           => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);

        public static bool IsEditor(this ClaimsPrincipal user)
            => user.IsInRole("Editor"); // Проверява дали потребителят е "Editor"

        public static string DisplayRole(this ClaimsPrincipal user)
        {
            if (user.IsInRole("Administrator")) return "Администратор";
            if (user.IsInRole("Editor")) return "Мениджър";
            return "Гост";
        }
    }
}
