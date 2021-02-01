using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Authentication
{
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    [Route("api/account/login")]
    public class LoginCommandHandler : Controller
    {
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(ILogger<LoginCommandHandler> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Execute([FromQuery] string? returnUrl, CancellationToken ct)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _logger.LogInformation("User logged in. Access token:\n{AccessToken}", accessToken);
            return LocalRedirect(returnUrl ?? "/fetch-data");
        }
    }
}
