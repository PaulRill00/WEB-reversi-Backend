using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReversiMvcApp.Data;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using ReversiMvcApp.Controllers;
using ReversiMvcApp.Hubs;
using ReversiMvcApp.Models;

namespace ReversiMvcApp
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
            // MVC Users Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(GetFilledConnectionString("DefaultConnection")));
            
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var dbContextUsers = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
            dbContextUsers.Database.Migrate();

            // Reversi Users Database
            services.AddDbContext<ReversiDbContext>(options =>
                options.UseSqlServer(GetFilledConnectionString("ReversiConnection")));

            var dbContextReversi = services.BuildServiceProvider().GetRequiredService<ReversiDbContext>();
            dbContextReversi.Database.Migrate();

            // Configure remaining
            services.AddTransient<PlayerController>();
            services.AddTransient<ApiController>();
            services.AddTransient<GoogleAuthenticator>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSignalR();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async (context) =>
                    {
                        context.HttpContext.Response.Redirect("https://paul.hbo-ict.org/Identity/Account/Login");
                    }
                };
            });
        }

        private string GetFilledConnectionString(string name)
        {
            var connectionString = Configuration.GetConnectionString(name);
            connectionString = connectionString.Replace("<SQLSource>", Environment.GetEnvironmentVariable("SQLSource"));
            connectionString = connectionString.Replace("<SQLPass>", Environment.GetEnvironmentVariable("SQLPass"));

            return connectionString;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<GameHub>("/gamehub");
            });
        }
    }
}
