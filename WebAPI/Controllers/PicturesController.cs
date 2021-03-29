using Application.Pictures.Commands;
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
