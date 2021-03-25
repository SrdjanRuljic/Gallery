using Application.Common.Behaviours;
using Application.Users.Queries.GetUserByUsername;
using Gallery.WebAPI.Auth;
using MediatR;
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
    public class AuthController : BaseController
    {
        public readonly IJwtFactory _jwtFactory;

        public AuthController(IJwtFactory jwtFactory)
        {
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            UserLoginDetailsViewModel result = await Mediator.Send(new GetUserByUsernameQuery 
            { 
                Username = model.Username 
            });

            if (result == null)
                return BadRequest("Incorect username or password.");

            if (!Hasher.VerifyPassword(model.Password, result.PasswordHash, result.PasswordSalt))
                return BadRequest("Incorect username or password.");

            object token = TokenHelper.GenerateJwt(result.Username, result.Role, _jwtFactory);

            return Ok(token);
        }
    }
}
