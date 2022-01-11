using ECSystem.Server.Main.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECSystem.Server.Main.Services {
    public class AuthService {
        //private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthService> _logger;

        public AuthService(ILogger<AuthService> logger) {
            //_context = context;
            _logger = logger;
        }

        public async Task<IdentityUser?> Authenticate(ApplicationDbContext _context, string username, string password) {

            return await _context.Users.Where(x => x.NormalizedUserName == username && x.PasswordHash == password).FirstOrDefaultAsync();
        
        
        }
    }
}
