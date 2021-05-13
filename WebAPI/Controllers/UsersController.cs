using Application.Users.Commands.Insert;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.LogedInUserData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class UsersController : BaseController
    {
        #region [GET]

        [HttpGet]
        [Route("data")]
        public async Task<IActionResult> GetLogedInUserData()
        {
            string username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            LogedInUserDataViewModel data = await Mediator.Send(new LogedInUserDataQuery()
            {
                Username = username
            });

            return Ok(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            List<GetAllUsersViewModel> users = await Mediator.Send(new GetAllUsersQuery());

            return Ok(users);
        }

        #endregion

        #region [POST]

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert(InsertUserCommand model)
        {
            long id = await Mediator.Send(model);

            return Ok(id);
        }

        #endregion
    }
}
