using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<ContactViewModel> list = _mapper.Map<List<ContactViewModel>>(await _contactsBusiness.GetAll());

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                ContactViewModel model = _mapper.Map<ContactViewModel>(await _contactsBusiness.GetById(id));

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        #endregion

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(ContactViewModel model)
        {
            bool isUpdated = false;

            try
            {
                await _contactsBusiness.Update(_mapper.Map<ContactModel>(model));

                return Ok(isUpdated);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        #endregion

        #region [POST]

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert(ContactViewModel model)
        {
            long id = 0;

            try
            {
                id = await _contactsBusiness.Insert(_mapper.Map<ContactModel>(model));
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        #endregion [POST]

        #region [DELETE]

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _contactsBusiness.Delete(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        #endregion
    }
}
