using Application.Codes.Queries.GetParents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodesController : BaseController
    {
        [HttpGet]
        [Route("parents/{languageId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(long languageId)
        {
            List<GetParentsCodesQueryViewModel> list = await Mediator.Send(new GetParentsCodesQuery
            {
                LanguageId = languageId
            });

            return Ok(list);
        }
    }
}
