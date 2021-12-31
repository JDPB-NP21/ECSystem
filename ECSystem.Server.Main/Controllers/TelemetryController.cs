using ECSystem.Server.Main.Data;
using ECSystem.Server.Main.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECSystem.Server.Main.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TelemetryController> _logger;

        public TelemetryController(ApplicationDbContext context, ILogger<TelemetryController> logger) {
            _context = context;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> PostDataTelemetry(TelemetryDto telemetryDto) {

            _logger.LogInformation(telemetryDto.ToString());

            return NoContent();
        }
    }
}
