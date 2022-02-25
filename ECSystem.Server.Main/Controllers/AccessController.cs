using ECSystem.Server.Main.Data;
using ECSystem.Server.Main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECSystem.Server.Main.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuth", Roles = "Administrator")]
    public class AccessController : ControllerBase {
        private readonly ApplicationDbContext _dbcontext;
        private readonly ILogger<AccessController> _logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccessController(ApplicationDbContext dbcontext,
            ILogger<AccessController> logger,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager) {

            _dbcontext = dbcontext;
            _logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        [HttpGet("getpos/{username}")]
        public async Task<IActionResult> GetPos([FromRoute] string username) {

            _logger.LogInformation(username.ToString());

            return this.Ok(username);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers() {
            var users = await userManager.Users.Select(user => user.UserName).ToListAsync();
            return this.Ok(users);
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] UserPasswordDto userPassword) {
            var result = await userManager.CreateAsync(new IdentityUser(userPassword.UserName), userPassword.Password);

            _logger.LogInformation(result.ToString());
            

            return this.Ok(result.ToString());
        }

        [HttpPost("role")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName) {
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));

            _logger.LogInformation(result.ToString());


            return this.Ok(result.ToString());
        }

        [HttpPost("addUserToRole")]
        public async Task<IActionResult> AddUserToRole([FromBody] UserRoleDto userRole) {
            var user = await userManager.FindByNameAsync(userRole.UserName);
            var result = await userManager.AddToRoleAsync(user, userRole.RoleName);

            _logger.LogInformation(result.ToString());


            return this.Ok(result.ToString());
        }
    }
}
