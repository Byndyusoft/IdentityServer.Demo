using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [AllowAnonymous]
    [Route("Example")]
    public class ExampleController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<string>> TokenGenerate([FromServices] AuthenticationTokenGenerator authenticationTokenGenerator)
        {
            return await authenticationTokenGenerator.GenerateTokenAndCallApiAsync();
        }
    }
}