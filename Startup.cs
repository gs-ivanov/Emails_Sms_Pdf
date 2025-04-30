namespace Emails_Sms_Pdf
{
    using DinkToPdf;
    using DinkToPdf.Contracts;
    using Emails_Sms_Pdf.Data;
    using Emails_Sms_Pdf.Data.Models;
    using Emails_Sms_Pdf.Infrastructure;
    using Emails_Sms_Pdf.Services.Email;
    using Emails_Sms_Pdf.Services.PDF;
    using Emails_Sms_Pdf.Services.Sms;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddDbContext<TurnirDbContext>(options => options
                .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TurnirDbContext>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            //services
            //    .AddTransient<IRankingService, RankingService>();
            //services
            //    .AddTransient<IMatchSchedulerService, MatchSchedulerService>();
            //services
            //    .AddTransient<IMatchResultNotifierService, MatchResultNotifierService>();
            services
                .AddTransient<IEmailSender, EmailSender>();
            services
                .AddTransient<ISmsSender, TwilioSmsSender>();
            services
                .AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services
                .AddTransient<PdfService>();
            //services
            //    .AddScoped<SignInManager<User>, CustomSignInManager<User>>();
            //services
            //    .Configure<TwilioSettings>(Configuration.GetSection("Twilio"));


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}