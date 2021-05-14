using Application.Users.Commands.Delete;
using Application.Users.Commands.Insert;
using Application.Users.Commands.Update;
using Application.Users.Commands.UpdatePassword;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetById;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            GetUserByIdViewModel user = await Mediator.Send(new GetUserByIdQuery
            {
                Id = id
            });

            return Ok(user);
        }

        #endregion

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(UpdateUserCommand model)
        {
            bool isUpdated = await Mediator.Send(model);

            return Ok(isUpdated);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("update-password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand command)
        {
            bool isUpdated = await Mediator.Send(command);

            return Ok(isUpdated);
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

        #region [DELETE]

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteUserCommand
            {
                Id = id
            });

            return Ok();
        }

        #endregion
    }
}
