using Application.Auth.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            object token = await Mediator.Send(command);

            return Ok(token);
        }
    }
}
