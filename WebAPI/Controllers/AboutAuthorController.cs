using Application.Author.Commands.Update;
using Application.Author.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
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

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(UpdateAboutAuthorCommand model)
        {
            bool isUpdated = await Mediator.Send(model);

            return Ok(isUpdated);
        }

        #endregion
    }
}
