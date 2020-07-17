using Gallery.BLL;
using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.WebAPI.Auth;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gallery.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IJwtFactory _jwtFactory;
        public readonly IUsersBusiness _usersBusiness;

        public LoginController(IJwtFactory jwtFactory, IUsersBusiness usersBusiness)
        {
            _jwtFactory = jwtFactory;
            _usersBusiness = usersBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            UserModel user = await _usersBusiness.GetByUsername(model.Username);

            if (user == null)
                return BadRequest(new { message = ErrorMessages.IncorectUsernameOrPassword });

            if (!_usersBusiness.VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest(new { message = ErrorMessages.IncorectUsernameOrPassword });

            object token = TokenHelper.GenerateJwt(user.Username, user.Role, _jwtFactory);

            return Ok(token);
        }
    }
}