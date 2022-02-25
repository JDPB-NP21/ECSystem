using Microsoft.AspNetCore.Identity;

namespace ECSystem.Server.Main.Data {
    public static class Seed {
        public static async void InitDb(IServiceProvider serviceProvider) {
            using (var scope = serviceProvider.CreateScope()) {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //If User or Role table is not empty, ignore Seeding. (Do only seeding if there is no data)
                if (dbContext.Users.Any() || dbContext.Roles.Any()) {
                    Console.WriteLine("Database not empty");
                    return;
                }
                

                var adminUser = new IdentityUser("admin") {
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(adminUser);

                if (!result.Succeeded)
                    throw new Exception("Failded creating admin user");

                //admin-admin 
                await userManager.AddPasswordAsync(adminUser, "admin");

                var adminRole = new IdentityRole("Administrator");
                var resultRole = await roleManager.CreateAsync(adminRole);
                if (!result.Succeeded)
                    throw new Exception("Failded creating admin role");

                adminUser = await userManager.FindByNameAsync("admin");
                await userManager.AddToRoleAsync(adminUser, "Administrator");

                await roleManager.CreateAsync(new IdentityRole("Victim"));

                Console.WriteLine("Done.");

            }

        }

    }
}
