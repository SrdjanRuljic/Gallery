using Application.Author.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutAuthorController : BaseController
    {
        #region [GET]

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(long id)
        {
            GetAboutAuthorByIdViewModel author = await Mediator.Send(new GetAboutAuthorByIdQuery
            {
                Id = id
            });

            return Ok(author);
        }

        #endregion
    }
}
