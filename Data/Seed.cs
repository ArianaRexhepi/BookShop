using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOOKS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Books.Data
{
    public static class Seed
    {
         public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {

                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

                var defaultUser = new ApplicationUser
                {
                    UserName = "Admin123",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };
                if (userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultUser, "Ariana123.");
                        await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                        await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    }

                }
            }
        }
    }
}