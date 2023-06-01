
using Microsoft.AspNetCore.Identity;
using Blazor4.Data.CustomProvider;
using Blazor4.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Blazor4.CustomProvider;
using FluentAssertions.Common;
using System.Security.Claims;
using static System.Formats.Asn1.AsnWriter;
using Blazor4;

namespace Blazor4
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMvc();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            IServiceCollection serviceCollection = builder.Services.AddDbContext<AdventureWorks2019Context>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();


            // Configure Identity
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
               .AddRoles<ApplicationRole>()
               .AddRoleManager<CustomRoleManager>()
               .AddUserManager<CustomUserManager>()
              .AddUserStore<CustomUserStore>()
              //.AddRoleStore<CustomRoleStore>()
              .AddEntityFrameworkStores<AdventureWorks2019Context>()
              .AddDefaultTokenProviders();
            // .AddSignInManager();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });

            builder.Services.AddLogging(builder =>
            {
                builder.AddFilter("Microsoft.AspNetCore.Authorization", LogLevel.Debug);
                builder.AddConsole();
            });


            // Add other services
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddTransient<ApplicationUsersTable>();
            //builder.Services.AddScoped<IUserStore<ApplicationUser>, CustomUserStore>();
            builder.Services.AddScoped<IRoleStore<ApplicationRole>, CustomRoleStore>();
            //builder.Services.AddScoped<RoleManager<ApplicationRole>>();
            builder.Services.AddScoped<RoleManager<ApplicationRole>, RoleManager<ApplicationRole>>();

            // builder.Services.AddScoped<RoleManager>();



            // Build the application
            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");




            app.Run();
        }
    }
}





