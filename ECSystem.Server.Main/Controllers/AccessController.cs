using ECSystem.Server.Main.Data;
using ECSystem.Server.Main.Models;
using ECSystem.Server.Main.Services;
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
        private readonly LocationService locationService;

        public AccessController(ApplicationDbContext dbcontext,
            ILogger<AccessController> logger,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            LocationService locationService) {

            _dbcontext = dbcontext;
            _logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.locationService = locationService;
        }


        [HttpGet("getpos/{username}")]
        public async Task<IActionResult> GetPos([FromRoute] string username) {

            _logger.LogInformation(username.ToString());

            var user = await userManager.FindByNameAsync(username);

            var result = await locationService.GetLatestPosition(_dbcontext, user);

            return this.Ok(result);
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
