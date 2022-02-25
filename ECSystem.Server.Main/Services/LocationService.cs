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
            List<DeviceLogs> logs = await dbContext.DeviceLogs.AsNoTracking()
                .Where(n => n.User.Equals(user))
                .TakeLast(1)
                .ToListAsync();

            //foreach (var log in logs) {
            //    JObject telm = JObject.Parse(log.Log);
            //    _logger.LogInformation(telm.ToString());
            //}


            return logs[0].Log.GeoLocation;
        }
    }
}
