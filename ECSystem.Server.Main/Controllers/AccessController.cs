using ECSystem.Server.Main.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECSystem.Server.Main.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccessController> _logger;
        public AccessController(ApplicationDbContext context, ILogger<AccessController> logger) {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetPos(string data) {

            _logger.LogInformation(data.ToString());

            return NoContent();
        }
    }
}
