namespace Emails_Sms_Pdf.Data
{
    using Emails_Sms_Pdf.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class TurnirDbContext : IdentityDbContext<User>
    {
        public TurnirDbContext(DbContextOptions<TurnirDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
