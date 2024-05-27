using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using StudentWebAPI.Models;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace StudentWebAPI.Configuration
{
    public class CustomAuthHandler : AuthenticationHandler<CustomAuthenticationOptions>
    {
        private readonly StudentAppContext _context;

        public CustomAuthHandler(IOptionsMonitor<CustomAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            StudentAppContext context
            ) : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if (!authorizationHeader.StartsWith("token", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var userParents = authorizationHeader.Split(' ', 2);

            if(userParents.Length != 2)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var user = _context.Students.FirstOrDefault(x => x.Email == userParents[0] && x.Password == userParents[1]);

            if(user == null)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string token = authorizationHeader.Substring("token".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            if(token != "12345")
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            //var client = await _context.Clients.FirstOrDefaultAsync(x => x.Token == token);

            //if (client == null)
            //{
            //    return AuthenticateResult.Fail("Unauthorized");
            //}

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "user")
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}