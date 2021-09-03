using ContentManagement.Identity.IdentityContext;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagement.Identity.RoleSeeder
{
    public static class Seeder
    {
        public async static Task SeedValues(RoleManager<IdentityRole> roleManager,ApplicationDbContext context)
        {

            context.Database.EnsureCreated();

            await AddRoles(roleManager, context);
        }

        private async static Task AddRoles(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            if (!roleManager.Roles.Any())
            {
                var listOfRoles = new List<IdentityRole>
                {
                    new IdentityRole("admin"),
                    new IdentityRole("generalUser"),
                };

                foreach (var role in listOfRoles)
                {
                    await roleManager.CreateAsync(role);
                }

                await context.SaveChangesAsync();
            }
        }

    }
}
