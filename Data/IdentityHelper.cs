using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKS.Areas.Identity.Data
{
    public class IdentityHelper
    {
        public static void Initialize(IServiceProvider provider)
        {
            var _context = provider.GetRequiredService<ApplicationDbContext>();
            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
        }

    }
}
