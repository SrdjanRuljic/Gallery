using AutoMapper;
using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gallery.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;
        private readonly IMapper _mapper;

        public UsersController(IUsersBusiness usersBusiness, IMapper mapper)
        {
            _usersBusiness = usersBusiness;
            _mapper = mapper;
        }

        #region [GET]

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        [Route("data")]
        public async Task<IActionResult> GetLogedInUserData()
        {
            string username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            LogedInUserDataViewModel data = _mapper.Map<LogedInUserDataViewModel>(await _usersBusiness.GetLogedInUserData(username));

            return Ok(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            List<UserListVirewModel> users = _mapper.Map<List<UserListVirewModel>>(await _usersBusiness.GetAll());

            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            UpdateUserViewModel user = _mapper.Map<UpdateUserViewModel>(await _usersBusiness.GetById(id));

            return Ok(user);
        }

        #endregion

        #region [POST]

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert(InsertUserViewModel model)
        {

            long id = await _usersBusiness.Insert(_mapper.Map<UserModel>(model));

            return Ok(id);
        }

        #endregion

        #region [PUT]

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(UpdateUserViewModel model)
        {
            bool isUpdated = await _usersBusiness.Update(_mapper.Map<UserModel>(model));

            return Ok(isUpdated);
        }

        #endregion

        #region [DELETE]

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _usersBusiness.Delete(id);

            return Ok();
        }

        #endregion

    }
}