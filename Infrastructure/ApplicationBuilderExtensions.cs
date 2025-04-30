using Emails_Sms_Pdf;

namespace Emails_Sms_Pdf.Infrastructure
{
    using Emails_Sms_Pdf.Data;
    using Emails_Sms_Pdf.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedRolesAndAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<TurnirDbContext>();
            data.Database.Migrate();
        }

        private static void SeedRolesAndAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    // Създаване на роли, ако не съществуват
                    string[] roleNames = { AdministratorRoleName, "Editor" };

                    foreach (var roleName in roleNames)
                    {
                        if (!await roleManager.RoleExistsAsync(roleName))
                        {
                            await roleManager.CreateAsync(new IdentityRole(roleName));
                        }
                    }

                    // Създаване на Администратор
                    const string adminEmail = "admin@tur.com";
                    const string adminPassword = "123";

                    var adminUser = await userManager.FindByEmailAsync(adminEmail);
                    if (adminUser == null)
                    {
                        var user = new User
                        {
                            Email = adminEmail,
                            UserName = adminEmail,
                            FullName = "Admin"
                        };

                        await userManager.CreateAsync(user, adminPassword);
                        await userManager.AddToRoleAsync(user, AdministratorRoleName);
                    }

                    // (Опционално) Създаване на Editor акаунт за тестове
                    const string editorEmail = "editor@tur.com";
                    const string editorPassword = "123";

                    var editorUser = await userManager.FindByEmailAsync(editorEmail);
                    if (editorUser == null)
                    {
                        var user = new User
                        {
                            Email = editorEmail,
                            UserName = editorEmail,
                            FullName = "Editor"
                        };

                        await userManager.CreateAsync(user, editorPassword);
                        await userManager.AddToRoleAsync(user, "Editor");
                    }
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
