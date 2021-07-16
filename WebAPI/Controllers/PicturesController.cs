using Application.Common.Pagination.Models;
using Application.Pictures.Commands.Delete;
using Application.Pictures.Commands.Insert;
using Application.Pictures.Commands.Search;
using Application.Pictures.Commands.Update;
using Application.Pictures.Queries.GetById;
using Application.Pictures.Queries.GetDetailsById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class PicturesController : BaseController
    {
        #region [GET]

        [HttpGet]
        [Route("single/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleById(long id)
        {
            GetDetailsByIdViewModel model = await Mediator.Send(new GetDetailsByIdQuery
            {
                Id = id
            });

            return Ok(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            GetPictureByIdViewModel picture = await Mediator.Send(new GetPictureByIdQuery
            {
                Id = id
            });

            return Ok(picture);
        }

        #endregion

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
        public async Task<IActionResult> Search(SearchPicturesCommand command)
        {
            PaginationResultViewModel<SearchPicturesCommandViewModel> pictures = await Mediator.Send(command);

            return Ok(pictures);
        }

        #endregion [POST]

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(UpdatePictureCommand model)
        {
            bool isUpdated = await Mediator.Send(model);

            return Ok(isUpdated);
        }

        #endregion

        #region [DELETE]

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            await Mediator.Send(new DeletePictureCommand
            {
                Id = id
            });

            return Ok();
        }

        #endregion
    }
}
