using Gallery.WebAPI.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IJwtFactory _jwtFactory;

        public AuthController(IJwtFactory jwtFactory)
        {
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            object token = TokenHelper.GenerateJwt("AA", "AA", _jwtFactory);

            return Ok(token);
        }
    }
}
