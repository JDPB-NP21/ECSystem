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
                
                //User creation
                var result = await userManager.CreateAsync(new IdentityUser("admin") {
                    EmailConfirmed = true,
                }, "admin");

                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Code);

                //Role creation
                var resultRole = await roleManager.CreateAsync(new IdentityRole("Administrator"));

                if (!resultRole.Succeeded)
                    throw new Exception("Failded creating admin role");


                var adminUser = await userManager.FindByNameAsync("admin");
                await userManager.AddToRoleAsync(adminUser, "Administrator");

                await roleManager.CreateAsync(new IdentityRole("TelemetryClient"));

                Console.WriteLine("InitDb Done.");
            }

        }

    }
}
