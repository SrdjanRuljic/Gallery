using Application.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
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
