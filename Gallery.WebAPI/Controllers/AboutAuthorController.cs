using AutoMapper;
using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gallery.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AboutAuthorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAboutAuthorBusiness _aboutAuthorBusiness;

        public AboutAuthorController(IMapper mapper, IAboutAuthorBusiness aboutAuthorBusiness)
        {
            _mapper = mapper;
            _aboutAuthorBusiness = aboutAuthorBusiness;
        }

        #region [GET]

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(long id)
        {
            AboutAuthorViewModel author = _mapper.Map<AboutAuthorViewModel>(await _aboutAuthorBusiness.GetById(id));

            return Ok(author);
        }

        #endregion

        #region [POST]

        [HttpPost, DisableRequestSizeLimit]
        [Route("")]

        public async Task<IActionResult> Insert(AboutAuthorViewModel model)
        {
            long id = await _aboutAuthorBusiness.Insert(_mapper.Map<AboutAuthorModel>(model));

            return Ok(id);
        }

        #endregion [POST]

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(AboutAuthorViewModel model)
        {
            bool isUpdated = false;

            await _aboutAuthorBusiness.Update(_mapper.Map<AboutAuthorModel>(model));

            return Ok(isUpdated);
        }

        #endregion
    }
}
