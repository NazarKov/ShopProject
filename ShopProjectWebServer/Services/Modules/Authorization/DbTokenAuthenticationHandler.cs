using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using ShopProjectWebServer.Services.Modules.Authorization.Interface;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ShopProjectWebServer.Services.Modules.Authorization
{
    public class DbTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthorizationService _authService;

        public DbTokenAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthorizationService authService)
            : base(options, logger, encoder, clock)
        {
            _authService = authService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers["Authorization"].ToString()
                .Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
                return Task.FromResult(AuthenticateResult.Fail("No token"));

            if (!_authService.LoginToken(token))
                return Task.FromResult(AuthenticateResult.Fail("Invalid token"));

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "User")
        };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
