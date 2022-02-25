using ECSystem.Server.Main.Data;
using ECSystem.Server.Main.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ECSystem.Server.Main.Helpers {
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
        private readonly IOptionsMonitor<AuthenticationSchemeOptions> options;
        private readonly ILoggerFactory logger;
        private readonly UrlEncoder encoder;
        private readonly ISystemClock clock;
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> userClaimsPrincipalFactory;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IUserClaimsPrincipalFactory<IdentityUser> userClaimsPrincipalFactory)
            : base(options, logger, encoder, clock) {
            this.options = options;
            this.logger = logger;
            this.encoder = encoder;
            this.clock = clock;
            this.context = context;
            this.userManager = userManager;
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");
            
            IdentityUser? user = null;

            try {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                
                user = await userManager.FindByNameAsync(username);
                if(user != null && !await userManager.CheckPasswordAsync(user, password))
                    user = null;
                
            } catch {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claimsPrincipal = await userClaimsPrincipalFactory.CreateAsync(user);
            var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
