using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using ReversiRestAPI.DAL;
using ReversiRestAPI.Interfaces;
using ReversiRestAPI.Models.Database;

namespace ReversiRestAPI
{
    public class Startup
    {
        readonly string MyCors = "_myCors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = "Data Source=<SQLSource>; Initial Catalog=ReversiDbRestApi; User ID=SA; Password=<SQLPass>";
            connectionString = connectionString.Replace("<SQLSource>", Environment.GetEnvironmentVariable("SQLSource"));
            connectionString = connectionString.Replace("<SQLPass>", Environment.GetEnvironmentVariable("SQLPass"));

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyCors, builder =>
                {
                    builder.WithOrigins("http://localhost", "http://localhost:3000", "http://localhost:53337", "https://paul.hbo-ict.org")
                        .WithMethods("GET", "PUT", "POST")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var dbContext = services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
            services.AddSingleton(typeof(IGameRepository), new GameAccessLayer(dbContext));

            dbContext.Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseCors(MyCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
