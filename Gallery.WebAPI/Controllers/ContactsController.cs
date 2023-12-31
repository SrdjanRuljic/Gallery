﻿using AutoMapper;
using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContactsBusiness _contactsBusiness;

        public ContactsController(IMapper mapper,
                                  IContactsBusiness contactsBusiness)
        {
            _mapper = mapper;
            _contactsBusiness = contactsBusiness;
        }

        #region [GET]

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            List<ContactViewModel> list = _mapper.Map<List<ContactViewModel>>(await _contactsBusiness.GetAll());

            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            ContactViewModel model = _mapper.Map<ContactViewModel>(await _contactsBusiness.GetById(id));

            return Ok(model);
        }

        #endregion

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(ContactViewModel model)
        {
            bool isUpdated = await _contactsBusiness.Update(_mapper.Map<ContactModel>(model));

            return Ok(isUpdated);
        }

        #endregion

        #region [POST]

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert(ContactViewModel model)
        {
            long id = await _contactsBusiness.Insert(_mapper.Map<ContactModel>(model));

            return Ok(id);
        }

        #endregion [POST]

        #region [DELETE]

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _contactsBusiness.Delete(id);

            return Ok();
        }

        #endregion
    }
}
