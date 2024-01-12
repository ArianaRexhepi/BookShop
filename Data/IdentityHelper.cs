using Microsoft.AspNetCore.Identity;

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
