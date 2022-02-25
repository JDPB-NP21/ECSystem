using ECSystem.Server.Main.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECSystem.Server.Main.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuth", Roles = "Administrator")]
    public class AccessController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccessController> _logger;
        public AccessController(ApplicationDbContext context, ILogger<AccessController> logger) {
            _context = context;
            _logger = logger;
        }


        [HttpGet("getpos/{userid}")]
        public async Task<IActionResult> GetPos([FromRoute] string userid) {

            _logger.LogInformation(userid.ToString());

            return this.Ok(userid);
        }
    }
}
