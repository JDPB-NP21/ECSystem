using ECSystem.Server.Main.Data;
using ECSystem.Server.Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ECSystem.Server.Main.Services {
    public class LocationService {
        private readonly ILogger<LocationService> _logger;

        public LocationService(ILogger<LocationService> logger) {
            _logger = logger;
        }

        public async Task<Location> GetLatestPosition(ApplicationDbContext dbContext, IdentityUser user) {
            var log = await dbContext.DeviceLogs.AsNoTracking()
                .Where(n => n.User.Equals(user))
                .OrderByDescending(s => s.FieldDateCreated)
                .FirstAsync();

            //foreach (var log in logs) {
            //    JObject telm = JObject.Parse(log.Log);
            //    _logger.LogInformation(telm.ToString());
            //}


            return log.Log.Location;
        }
    }
}
