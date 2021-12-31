using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECSystem.Server.Main.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        DbSet<StaticDevice> staticDevices;
        DbSet<DeviceLog> deviceLogs;

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }
    }
}
