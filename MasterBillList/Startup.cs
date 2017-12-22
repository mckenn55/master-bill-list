using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MasterBillList.Data;
using MasterBillList.Models;
using MasterBillList.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MasterBillList
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Tokens.ChangePhoneNumberTokenProvider = "Phone";

                config.SignIn.RequireConfirmedEmail = true;
                //config.SignIn.RequireConfirmedPhoneNumber = true;
                
                config.Password.RequiredLength = 20;
                config.Password.RequireUppercase = true;
                config.Password.RequireLowercase = true;
                config.Password.RequiredUniqueChars = 6;

                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Lockout.AllowedForNewUsers = true;

                config.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Google OAuth 2.0
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MasterBillList";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.Configure<AuthMessageSenderOptions>(Configuration);
            
            services.Configure<TwilioSenderOptions>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
