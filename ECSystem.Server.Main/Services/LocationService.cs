using ECSystem.Server.Main.Data;

namespace ECSystem.Server.Main.Services {
    public class LocationService {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LocationService> _logger;
        public LocationService(ApplicationDbContext context, ILogger<LocationService> logger) {
            _context = context;
            _logger = logger;
        }
    }
}
