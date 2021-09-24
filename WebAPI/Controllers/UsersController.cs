using Application.Common.Pagination.Models;
using Application.Users.Commands.Delete;
using Application.Users.Commands.Insert;
using Application.Users.Commands.Update;
using Application.Users.Commands.UpdatePassword;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetById;
using Application.Users.Queries.LogedInUserData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            GetUserByIdViewModel user = await Mediator.Send(new GetUserByIdQuery
            {
                Id = id
            });

            return Ok(user);
        }

        #endregion

        #region [PUT]

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(UpdateUserCommand model)
        {
            await Mediator.Send(model);

            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut]
        [Route("update-password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand command)
        {
            bool isUpdated = await Mediator.Send(command);

            return Ok(isUpdated);
        }

        #endregion

        #region [POST]

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetAll(GetAllUsersQuery query)
        {
            PaginationResultViewModel<GetAllUsersViewModel> users = await Mediator.Send(query);

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert(InsertUserCommand model)
        {
            string id = await Mediator.Send(model);

            return Ok(id);
        }

        #endregion

        #region [DELETE]

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
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
