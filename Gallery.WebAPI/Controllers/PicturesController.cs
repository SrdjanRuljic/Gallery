using AutoMapper;
using Gallery.BLL.Interfaces;
using Gallery.DTO;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        #region [GET]

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(long id)
        {
            PictureViewModel picture = _mapper.Map<PictureViewModel>(await _picturesBusiness.GetById(id));

            return Ok(picture);
        }

        #endregion

        #region [POST]

        [HttpPost, DisableRequestSizeLimit]
        [Route("")]

        public async Task<IActionResult> Insert(PictureViewModel model)
        {
            long id = await _picturesBusiness.UploadAndInsert(_mapper.Map<PicturesDTO>(model));

            return Ok(id);
        }

        [HttpPost]
        [Route("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(PictureSearchViewModel model)
        {
            List<PictureViewModel> pictures = _mapper.Map<List<PictureViewModel>>(await _picturesBusiness.Search(model.Name,
                                                                                                                 model.CategoryId));

            return Ok(pictures);
        }

        #endregion [POST]

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(PictureViewModel model)
        {
            bool isUpdated = false;

            await _picturesBusiness.Update(_mapper.Map<PicturesDTO>(model));

            return Ok(isUpdated);
        }

        #endregion

        #region [DELETE]

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            await _picturesBusiness.Delete(id);

            return Ok();
        }

        #endregion
    }
}
