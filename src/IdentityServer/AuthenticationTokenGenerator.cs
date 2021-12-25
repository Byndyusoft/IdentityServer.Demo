using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer4;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class AuthenticationTokenGenerator
    {
        private readonly ILogger<AuthenticationTokenGenerator> _logger;
        private readonly IdentityServerTools _identityServerTools;

        public AuthenticationTokenGenerator(
            ILogger<AuthenticationTokenGenerator> logger,
            IdentityServerTools identityServerTools)
        {
            _logger = logger;
            _identityServerTools = identityServerTools;
        }

        public async Task<string> GenerateTokenAndCallApiAsync()
        {
            var jwtToken =
                await _identityServerTools.IssueClientJwtAsync("IS4", 600, new[] {"ISScope", "authentication"},
                    new[] {"Api"});

            using var client = new HttpClient();
            client.SetBearerToken(jwtToken);

            using var response = await client.GetAsync("http://localhost:11011/auth");
            if (!response.IsSuccessStatusCode)
            {
                var message = $"Not Successful {response.StatusCode}";
                _logger.LogInformation(message);
                return message;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var message = $"Successful {content}";
                _logger.LogInformation(message);
                return message;
            }
        }
    }
}