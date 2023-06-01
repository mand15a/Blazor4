using Blazor4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Blazor4.Data.CustomProvider
{
    public class CustomRoleManager : RoleManager<ApplicationRole>
    {
        //private readonly IServiceProvider _serviceProvider;
        string[] adminEmails = { "rob0@adventure-works.com", "roc0@adventure-works.com", "rus0@adventure-works.com" };
        string adminRole = "Admin";
        public CustomRoleManager(IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
            async Task ConfigureAsync(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
            {

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    foreach (string email in adminEmails)
                    {
                        if (await userManager.FindByEmailAsync(email) == null)
                        {
                            var user = new ApplicationUser
                            {
                                UserName = email,
                                Email = email
                            };

                            //  await userManager.CreateAsync((ApplicationUser)user, "Test1234");
                            await userManager.AddToRoleAsync((ApplicationUser)user, adminRole);

                        }
                    }
                }
            }
        }
    }
}




