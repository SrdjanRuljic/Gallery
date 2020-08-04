using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IMapper _mapper;

        public PicturesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region [POST]

        [HttpPost, DisableRequestSizeLimit]
        [Route("")]
        public async Task<IActionResult> Insert(PictureViewModel model)
        {
            long id = 0;

            try
            {
                //id = await _categoryBusiness.Insert(_mapper.Map<CategoryModel>(model));
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        #endregion [POST]
    }
}
