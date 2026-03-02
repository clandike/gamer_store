using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GamerStore.Data;

namespace GamerStore.Models
{
    public static class IdentitySeedData
    {
        private const string AdminUser = "Admin";
        private const string AdminPassword = "Secret123$";

        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppIdentityDbContext context = scope.ServiceProvider
                .GetRequiredService<AppIdentityDbContext>();

                var migrations = await context.Database.GetPendingMigrationsAsync();

                if (migrations.Any())
                {
                    await context.Database.MigrateAsync();
                }

                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                IdentityUser? user = await userManager.FindByNameAsync(AdminUser);

                if (user is null)
                {
                    user = new IdentityUser("Admin")
                    {
                        Email = "admin@example.com",
                        PhoneNumber = "555-1234",
                    };

                    await userManager.CreateAsync(user, AdminPassword);
                }
            }
        }
    }
}
