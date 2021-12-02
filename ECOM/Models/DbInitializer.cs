using ECOM.Areas.Identity.Data;
using ECOM.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.Models
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ECOMContext context, IServiceProvider serviceProvider
            ,UserManager<ECOMUser> userManager)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] RoleNames = { "Admin", "User" };
            IdentityResult roleResult;
            foreach (var roleName in RoleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            string Email = "support@ecomsite.com";
            string password = "Support,./123";
            if (userManager.FindByEmailAsync(Email).Result==null)
            {
                ECOMUser user = new ECOMUser();
                user.UserName = Email;
                user.Email = Email;
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
