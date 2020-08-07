using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gallery.BLL.Interfaces;
using Gallery.DTO;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPicturesBusiness _picturesBusiness;

        public PicturesController(IMapper mapper,
                                  IPicturesBusiness picturesBusiness)
        {
            _mapper = mapper;
            _picturesBusiness = picturesBusiness;
        }

        #region [POST]

        [HttpPost, DisableRequestSizeLimit]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Insert(PictureViewModel model)
        {
            long id = 0;

            try
            {
                var result = await _picturesBusiness.UploadAndInsert(_mapper.Map<PicturesDTO>(model));

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search(PictureSearchViewModel model)
        {
            List<PictureViewModel> pictures = _mapper.Map<List<PictureViewModel>>(await _picturesBusiness.Search(model.Name, 
                                                                                                                 model.CategoryId));

            return Ok(pictures);
        }

        #endregion [POST]
    }
}
