using Application.Pictures.Commands;
using Application.Pictures.Commands.InsertCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : BaseController
    {
        #region [POST]

        [HttpPost, DisableRequestSizeLimit]
        [Route("")]
        public async Task<IActionResult> Insert(InsertPictureCommand model)
        {
            long id = await Mediator.Send(model);

            return Ok(id);
        }

        [HttpPost]
        [Route("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(SearchCommand command)
        {
            List<SearchCommandViewModel> pictures = await Mediator.Send(command);

            return Ok(pictures);
        }

        #endregion [POST]
    }
}
